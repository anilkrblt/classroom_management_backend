using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Contracts;

namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(
                options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()    //   WithOrigins("https://example.com")
                            .AllowAnyMethod()   //   WithMethods("POST", "GET")
                            .AllowAnyHeader() //   WithHeaders("accept", "content-type")
                                                
                            .WithExposedHeaders("X-Pagination")); //to enable the client application to read the 
                                                                  //new X-Pagination  header that we’ve added in our action


                        
            });

        public static void ConfigureISSIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
                /*
                We do not initialize any of the properties inside the options because we 
                are fine with the default values for now.
                Sayfa 9
                */
            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();


        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();





  

    }
}