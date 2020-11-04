using System;
using System.Linq;
using System.Threading.Tasks;

using MassTransit;

using MassTut.Commons.Events.Locations;
using MassTut.Commons.Repositories;

using Microsoft.Extensions.Logging;

namespace MassTut.Keeper.Infrastructure
{
    /// <summary>
    /// Handler for Location Commands.
    /// </summary>
    public class LocationEventHandler : IConsumer<CreateLocation>, IConsumer<IRequestLocations>
    {
        private readonly ILogger<LocationEventHandler> _logger;
        private readonly ILocationRepository _locationRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public LocationEventHandler(ILogger<LocationEventHandler> logger, ILocationRepository locationRepository, IPublishEndpoint publishEndpoint)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public Task Consume(ConsumeContext<CreateLocation> context)
        {
            _logger.LogDebug("Received location {location} from MassTransit", context.Message);
            return _locationRepository.Create(context.Message, context.CancellationToken);
        }

        public async Task Consume(ConsumeContext<IRequestLocations> context)
        {
            _logger.LogInformation("Someone requested all locations!");
            var result = await _locationRepository
                .GetAll(context.CancellationToken)
                .ToListAsync(context.CancellationToken);
            await _publishEndpoint.Publish(new LocationsReceivedEvent
            {
                Locations = result
            });
        }
    }
}
