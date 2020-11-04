using System.Collections.Generic;

using MassTut.Commons.Entities;

namespace MassTut.Commons.Events.Locations
{
    /// <summary>
    /// Command to request all locations.
    /// </summary>
    public interface IRequestLocations
    {
    }

    /// <summary>
    /// Event fired when requested Locations are sent.
    /// </summary>
    public class LocationsReceivedEvent
    {
        public IEnumerable<Location> Locations { get; set; }
    }
}
