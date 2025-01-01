using System.Text;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Service;
using Service.Contracts;

namespace ClassroomManagement.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(
                options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithExposedHeaders("X-Pagination"));
            });

        public static void ConfigureISSIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();


        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
                    services.AddDbContext<RepositoryContext>(opts =>
                                                             opts.UseSqlite(configuration.GetConnectionString("sqliteConnection")));


        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Key"];
            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new ArgumentNullException(nameof(secretKey), "JWT secret key cannot be null or empty");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {


                var jwtSettings = configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings["Key"];

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // Authorization header'ın ham halini yazalım.
                        var rawHeader = context.Request.Headers["Authorization"].ToString();
                        Console.WriteLine("RAW AUTH HEADER: " + rawHeader);

                        // "Bearer " ile başlıyorsa substring alalım.
                        if (!string.IsNullOrEmpty(rawHeader) && rawHeader.StartsWith("Bearer "))
                        {
                            context.Token = rawHeader.Substring("Bearer ".Length).Trim();
                            Console.WriteLine("SET context.Token: " + context.Token);
                        }

                        return Task.CompletedTask;
                    },

                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);

                        // Hata anında Authorization header’ını tekrar okuyabilirsiniz:
                        var rawHeader = context.Request.Headers["Authorization"].ToString();
                        Console.WriteLine("At fail time, rawHeader = " + rawHeader);

                        // İsterseniz "Bearer " kısmını ayıklayabilirsiniz:
                        if (!string.IsNullOrEmpty(rawHeader) && rawHeader.StartsWith("Bearer "))
                        {
                            var token = rawHeader.Substring("Bearer ".Length).Trim();
                            Console.WriteLine("At fail time, token = " + token);
                        }

                        return Task.CompletedTask;
                    }

                };



                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }
        public static void ConfigureTokenService(this IServiceCollection services) =>
            services.AddScoped<ITokenService, TokenService>();

    }
}