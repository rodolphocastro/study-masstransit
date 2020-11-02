namespace MassTut.Commons.Entities
{
    /// <summary>
    /// A location.
    /// </summary>
    public class Location
    {
        protected Location()
        {

        }

        public Location(long id, string title, double lat, double longi)
        {
            Id = id;
            Title = title;
            Latitude = lat;
            Longitude = longi;
        }

        public bool IsNull => this is NullIsland;

        public long Id { get; protected set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        /// <summary>
        /// Nullable object for Locations.
        /// </summary>
        public static Location Null { get; } = new NullIsland();

        /// <summary>
        /// A null location
        /// </summary>
        class NullIsland : Location
        {
            public NullIsland()
            {
                Id = -1;
                Title = "Null Island";
                Latitude = -999d;
                Longitude = -999d;
            }
        }
    }

}
