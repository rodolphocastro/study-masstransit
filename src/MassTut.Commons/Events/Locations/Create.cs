using System;

using MassTut.Commons.Entities;

namespace MassTut.Commons.Events.Locations
{
    /// <summary>
    /// Command to create a new Location.
    /// </summary>
    public class CreateLocation : Location
    {
        public CreateLocation()
        {
            Id = 0;
        }
    }

    /// <summary>
    /// Event fired when a new Location was created somewhere in the system.
    /// </summary>
    public class LocationCreatedEvent
    {
        public long Id { get; set; }
        public DateTimeOffset SavedOn { get; private set; } = DateTimeOffset.UtcNow;
    }
}
