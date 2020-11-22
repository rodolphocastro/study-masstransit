using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using MassTut.Commons.Events.Locations;
using MassTut.Commons.Repositories;
using MassTut.Keeper.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MassTut.Keeper
{
    /// <summary>
    /// Worker that constantly publishes a new Command for creating Locations.
    /// </summary>
    public class PublisherWorker : BackgroundService
    {
        private readonly ILogger<PublisherWorker> _logger;
        private readonly ILocationRepository _locationRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly LocationGenerator _generator;

        public PublisherWorker(ILogger<PublisherWorker> logger, ILocationRepository locationRepository, IServiceProvider serviceProvider, LocationGenerator generator)
        {
            _logger = logger;
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                using (var scope = _serviceProvider.CreateScope())
                {
                    var publisher = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
                    var subject = _generator.FetchOne();
                    await publisher.Publish(new CreateLocation 
                    { 
                        Latitude = subject.Latitude, 
                        Longitude = subject.Longitude, 
                        Title = subject.Title 
                    }, stoppingToken);
                }
                _logger.LogInformation("There are {locationCount} locations on the System", await _locationRepository.GetAll(stoppingToken).CountAsync(stoppingToken));
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
