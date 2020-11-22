using Microsoft.Extensions.Hosting;

namespace MassTut.Keeper
{
    public class Program
    {
        protected WorkerStartup Startup { get; set; } = null;

        public static void Main(string[] args)
        {
            var self = new Program();
            self.CreateHostBuilder(args).Build().Run();
        }

        public IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    Startup = new WorkerStartup(hostContext.Configuration, services);
                    Startup.Configure();
                });
    }
}
