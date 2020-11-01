using System.Globalization;
using LocalizeApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace LocalizeApi
{
    public static class LocalizeServiceExtensions
    {
        private static string _dbName = "LocalizationsDb";
        public static IServiceCollection AddLocalizationService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(_dbName);
            services.AddDbContext<LocalizationContext>(options =>
                options.UseSqlServer(connectionString));
            
            services.AddTransient<IStringLocalizer, EFStringLocalizer>();
            services.AddSingleton<IStringLocalizerFactory>(new EFStringLocalizerFactory(connectionString));
            services.AddControllersWithViews().AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(null);
            });
            return services;
        }

        public static IServiceCollection AddLocalizationService(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LocalizationContext>(options =>
                options.UseSqlServer(connectionString));
            
            services.AddTransient<IStringLocalizer, EFStringLocalizer>();
            services.AddSingleton<IStringLocalizerFactory>(new EFStringLocalizerFactory(connectionString));
            services.AddControllersWithViews().AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(null);
            });
            return services;
        }
        
        public static IApplicationBuilder AddLocalizationApp(this IApplicationBuilder app)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru"),
                new CultureInfo("de")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            
            return app;
        }
    }
}