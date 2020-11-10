namespace MassTut.Keeper.Infrastructure
{
    /// <summary>
    /// Settings for configuring the RabbitMQ bus.
    /// </summary>
    public class BusSettings
    {
        /// <summary>
        /// Key where this configuration is held on.
        /// </summary>
        public const string SettingsKey = "RabbitMQ";

        /// <summary>
        /// Default credentials.
        /// </summary>
        protected const string DefaultCredentials = "guest";

        /// <summary>
        /// Address to connect to the RabbitMQ instance.
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Virtual Host to be used.
        /// </summary>
        public string VirtualHost { get; set; } = "/";

        /// <summary>
        /// Username to authenticate with.
        /// </summary>
        public string Username { get; set; } = DefaultCredentials;

        /// <summary>
        /// Password to authenticate with.
        /// </summary>
        public string Password { get; set; } = DefaultCredentials;
    }
}
