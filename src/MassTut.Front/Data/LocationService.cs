using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using MassTut.Commons.Entities;
using MassTut.Commons.Events.Locations;

using Microsoft.Extensions.DependencyInjection;

namespace MassTut.Front.Data
{
    public class LocationEventHandler : IConsumer<LocationsReceivedEvent>
    {
        private readonly LocationService _locationService;

        public LocationEventHandler(LocationService locationService)
        {
            _locationService = locationService ?? throw new System.ArgumentNullException(nameof(locationService));
        }

        public Task Consume(ConsumeContext<LocationsReceivedEvent> context)
        {
            _locationService.Clear();
            foreach (var location in context.Message.Locations)
            {
                _locationService.Add(location);
            }
            return Task.CompletedTask;
        }
    }

    public class LocationService 
    {
        protected readonly ICollection<Location> _locations = new HashSet<Location>();
        private readonly IServiceProvider _serviceProvider;

        public LocationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task AskForLocations(CancellationToken ct = default)
        {
            using var scoped = _serviceProvider.CreateScope();
            await scoped.ServiceProvider
                .GetRequiredService<IPublishEndpoint>()
                .Publish<IRequestLocations>(new { }, ct);
        }

        public Task Add(Location location)
        {
            _locations.Add(location);
            return Task.CompletedTask;
        }

        public Task Clear()
        {
            _locations.Clear();
            return Task.CompletedTask;
        }
    }
}
