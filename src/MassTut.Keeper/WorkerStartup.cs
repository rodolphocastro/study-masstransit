using System;

using MassTransit;

using MassTut.Commons.Repositories;
using MassTut.Keeper.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MassTut.Keeper
{
    public class WorkerStartup
    {
        private IConfiguration _configuration;
        private IServiceCollection _services;

        public WorkerStartup(IConfiguration configuration, IServiceCollection services)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        protected internal virtual void Configure()
        {
            var busSettings = _configuration
                        .GetSection(BusSettings.SettingsKey)
                        .Get<BusSettings>();
            
            _services.AddMassTransit(x =>
            {
                x.AddConsumer<LocationEventHandler>();
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(busSettings.Host, busSettings.VirtualHost, h =>
                    {
                        h.Username(busSettings.Username);
                        h.Password(busSettings.Password);
                    });
                    cfg.ConfigureEndpoints(ctx);
                });
            });
            
            _services.AddMassTransitHostedService();

            _services.AddSingleton<ILocationRepository, LocationState>();
            _services.AddSingleton<LocationGenerator>();
            _services.AddHostedService<PublisherWorker>();
        }
    }
}