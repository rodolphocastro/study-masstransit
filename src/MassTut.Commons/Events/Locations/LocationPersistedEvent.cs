using System;

namespace MassTut.Commons.Events.Locations
{
    public class LocationPersistedEvent
    {
        public const long EmptyId = -1;

        public long Id { get; set; } = EmptyId;
        public DateTimeOffset SavedOn { get; private set; } = DateTimeOffset.UtcNow;
    }
}
