namespace MassTut.Keeper.Infrastructure
{
    /// <summary>
    /// Settings for configuring the RabbitMQ bus.
    /// </summary>
    public class BusSettings
    {        
        public const string SettingsKey = "RabbitMQ";

        public string Host { get; set; }        

        public string VirtualHost { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }        
    }
}
