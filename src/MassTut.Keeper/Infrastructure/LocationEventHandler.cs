using System;
using System.Threading.Tasks;

using MassTransit;

using MassTut.Commons.Events.Locations;
using MassTut.Commons.Repositories;

using Microsoft.Extensions.Logging;

namespace MassTut.Keeper.Infrastructure
{
    public class LocationEventHandler : IConsumer<LocationCreatedEvent>
    {
        private readonly ILogger<LocationEventHandler> _logger;
        private readonly ILocationRepository _locationRepository;

        public LocationEventHandler(ILogger<LocationEventHandler> logger, ILocationRepository locationRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        }

        public Task Consume(ConsumeContext<LocationCreatedEvent> context)
        {
            _logger.LogDebug("Received location {location} from MassTransit", context.Message);
            return _locationRepository.Create(context.Message, context.CancellationToken);
        }
    }
}
