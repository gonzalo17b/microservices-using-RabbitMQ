using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitsBurrow.Domain.Bus;
using RabbitsBurrow.IoC;
using RabbitsBurrow.Rabbit.Application.Rabbit.AddRabbit.Events;
using RabbitsBurrow.Rabbit.Data.Context;
using RabbitsBurrow.Rabbit.Data.Repositories;
using RabbitsBurrow.Rabbit.Domain.IRepository;
using RabbitsBurrow.Rabbit.Domain.Services;
using RabbitsBurrow.Rabbit.Domain.Services.Interfaces;

namespace RabbitsBurrow.Rabbit.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMediatR(typeof(Startup));

            services.AddSwaggerGen();

            services.AddDbContext<RabbitDbContext>(opt
                => opt.UseInMemoryDatabase(nameof(RabbitDbContext)).EnableSensitiveDataLogging());

            services.AddTransient<IRabbitRepository, RabbitRepository>();
            services.AddTransient<IRabbitService, RabbitService>();
            services.AddTransient<RabbitDbContext>();

            services.AddTransient<AddRabbitEventHandler>();
            services.AddTransient<IEventHandler<AddRabbitEvent>, AddRabbitEventHandler>();

            DependencyContainer.RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rabbit Microservice V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<AddRabbitEvent, AddRabbitEventHandler>();
        }
    }
}
