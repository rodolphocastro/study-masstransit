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

        public Location(string title, double lat, double longi)
        {
            Title = title;
            Latitude = lat;
            Longitude = longi;
        }

        public Location(long id, string title, double lat, double longi) : this(title, lat, longi)
        {
            Id = id;
        }

        public Location(long id, Location lhs) : this(id, lhs.Title, lhs.Latitude, lhs.Longitude)
        {

        }


        public bool IsNull => this is NullIsland;

        public long Id { get; protected set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString() => $"Location {Title} @ (lat: {Latitude},lon: {Longitude})";

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
