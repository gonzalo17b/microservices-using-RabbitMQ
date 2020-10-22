using Hellang.Middleware.ProblemDetails;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitsBurrow.Burrow.Application.Burrow.AddMember;
using RabbitsBurrow.Burrow.Data.Context;
using RabbitsBurrow.Burrow.Data.Repositories;
using RabbitsBurrow.Burrow.Domain.IRepository;
using RabbitsBurrow.Burrow.Domain.Services;
using RabbitsBurrow.Burrow.Domain.Services.Interfaces;
using RabbitsBurrow.IoC;

namespace RabbitsBurrow.Burrow.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMediatR(typeof(Startup));

            services.AddSwaggerGen();

            services.AddProblemDetails(setup =>
            {
                setup.IncludeExceptionDetails = (ctx, ex) =>
                {
                    var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                    return !env.IsProduction();
                };
            });

            DependencyContainer.RegisterServices(services);

            services.AddDbContext<BurrowDbContext>(opt
               => opt.UseInMemoryDatabase(nameof(BurrowDbContext)).EnableSensitiveDataLogging());

            services.AddTransient<IBurrowService, BurrowService>();
            services.AddTransient<IBurrowRepository, BurrowRepository>();
            services.AddTransient<BurrowDbContext>();
            services.AddTransient<IRequestHandler<AddMemberCommand, bool>, AddMemberCommandHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AddTestData(app);

            app.UseProblemDetails();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Barrow Microservice V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private static void AddTestData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BurrowDbContext>();

                var burrow = Domain.Model.Burrow.New();
                burrow.SetId(1);
                burrow.SetMaximumX(50);
                burrow.SetMaximumY(75);

                context.Burrows.Add(burrow);

                context.SaveChanges();
            }
        }
    }
}
