using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MassTut.Commons.Entities;

namespace MassTut.Commons.Repositories
{
    /// <summary>
    /// Describe methods to access Locations.
    /// </summary>
    public interface ILocationRepository
    {
        /// <summary>
        /// Gets a single Location by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Location> GetById(long id, CancellationToken ct = default);

        /// <summary>
        /// Gets locations by a title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        IAsyncEnumerable<Location> GetByTitle(string title, CancellationToken ct = default);

        /// <summary>
        /// Creates a new Location.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<long> Create(Location location, CancellationToken ct = default);

        /// <summary>
        /// Updates an existing location.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Update(Location location, CancellationToken ct = default);

        /// <summary>
        /// Deletes an existing location.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Delete(long id, CancellationToken ct = default);
    }
}
