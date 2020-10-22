using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitsBurrow.Domain.Bus;
using RabbitsBurrow.Infrastructure.Bus;

namespace RabbitsBurrow.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });
        }
    }
}
