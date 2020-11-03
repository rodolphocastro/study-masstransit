using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MassTut.Commons.Entities;
using MassTut.Commons.Repositories;

using Microsoft.Extensions.Logging;

namespace MassTut.Keeper.Infrastructure
{
    public class LocationState : ILocationRepository
    {
        private readonly ILogger<LocationState> _logger;
        private readonly ICollection<Location> _locations;

        protected virtual Location FetchLocation(long id) => _locations.SingleOrDefault(l => l.Id == id) ?? Location.Null;

        public LocationState(ILogger<LocationState> logger, ICollection<Location> locations = null)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _locations = locations ?? new HashSet<Location>();
        }

        public Task<long> Create(Location location, CancellationToken ct = default)
        {
            long highestId = _locations
                .DefaultIfEmpty(new Location(0, Location.Null))
                .Max(location => (long?)location.Id) ?? 0;
            var subject = new Location(highestId + 1, location);
            _logger.LogDebug("A new location {location} is being added to the storage", subject);
            _locations.Add(subject);
            return Task.FromResult(subject.Id);

        }

        public async Task Delete(long id, CancellationToken ct = default)
        {
            var subject = await GetById(id, ct);
            _logger.LogDebug("A location {location} is being remover from the storage", subject);
            _locations.Remove(subject);
        }

        public Task<Location> GetById(long id, CancellationToken ct = default)
        {
            return Task.FromResult(FetchLocation(id));
        }

        public IAsyncEnumerable<Location> GetByTitle(string title, CancellationToken ct = default)
        {
            return _locations
                .Where(l => l.Title.Contains(title, StringComparison.InvariantCultureIgnoreCase))
                .ToAsyncEnumerable();
        }

        public async Task Update(Location location, CancellationToken ct = default)
        {
            _logger.LogDebug("Updating location {location} on the storage", location);
            await Delete(location.Id, ct);
            _locations.Add(location);
        }

        public IAsyncEnumerable<Location> GetAll(CancellationToken ct = default)
        {
            return _locations
                .ToAsyncEnumerable();
        }
    }
}
