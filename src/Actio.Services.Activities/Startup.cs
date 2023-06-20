using Actio.Common.Commands;
using Actio.Services.Activities.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Actio.Common.RabbitMq;
using Actio.Common.Mongo;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Repositories;
using Actio.Services.Activities.Services;
using Actio.Common.Events;
using Microsoft.AspNetCore;
using Actio.Common.Auth;

namespace Actio.Services.Activities
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging();
            services.AddJwt(Configuration);
            services.AddMongoDB(Configuration);
            services.AddRabbitMq(Configuration);

            services.AddScoped<ICommandHandler<CreateActivity>, CreatedActivityHandler>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDatabaseSeeder, CustomMongoSeeder>();
            services.AddScoped<IActivityService, ActivityService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            //Iniciar Database
            IServiceScope scope = app.ApplicationServices.CreateScope();
            IDatabaseInitializer initSeed = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
            initSeed.InitizalizeAsync();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
