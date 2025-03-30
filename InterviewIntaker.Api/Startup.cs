using InterviewIntaker.Application.MappingProfiles;
using InterviewIntaker.Application.Task;
using InterviewIntaker.Data.Profiles;
using InterviewIntaker.Data.Repositories;
using InterviewIntaker.Data.DbContext;
using InterviewIntaker.Infrastructure.ServiceBus;
using Microsoft.EntityFrameworkCore;

namespace InterviewIntaker.Api
{
    /// <summary>
    /// Api startup
    /// </summary>
    /// <param name="services">Services</param>
    /// <param name="configuration">Configuration</param>
    public class Startup(IServiceCollection services, IConfiguration configuration)
    {
        public void Build()
        {
            ConfigureServices(services, configuration);
        }

        private void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configurationManager)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<TaskProfile>();
                config.AddProfile<TaskEntityProfile>();
            });

            services.AddScoped<ITaskRepository, TaskRepository>();
            serviceCollection.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CreateTaskCommandHandler).Assembly));

            var connectionString = configurationManager.GetConnectionString("Dev");
            serviceCollection.AddDbContext<AppDbContext>(config => config.UseSqlServer(connectionString, options =>
            {
                options.EnableRetryOnFailure();
            }));

            services.Configure<ServiceBusOptions>(configuration.GetSection("Azure:ServiceBus"));
            
            services.AddSingleton<IServiceBusHandler, ServiceBusHandler>();
        }
    }
}
