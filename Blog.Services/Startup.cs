using Blog.Models.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blog.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    // ReSharper disable once ClassNeverInstantiated.Global
    public class Startup
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        public Startup(IHostingEnvironment env)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true) //todo: change to false when implemented
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private IConfigurationRoot Configuration { get; } //was public in template

        // This method gets called by the runtime. Use this method to add services to the container.
        // ReSharper disable once UnusedMember.Local
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        // ReSharper disable once UnusedMember.Global
        public void ConfigureServices(IServiceCollection services)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            // Add framework services.
            services.AddMvc(config =>
              {
                  config.Filters.Add(new UncaughtExceptionFilter(loggerFactory, new SendGridAdapter()));
              });

            // Add application services.
            services.AddTransient<IMailProviderAdapter, SendGridAdapter>();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // ReSharper disable once UnusedMember.Local
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        // ReSharper disable once UnusedMember.Global
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            if (env.IsDevelopment())
            {
                //app.UseRuntimeInfoPage("/info"); // http://localhost/info
                app.UseSwagger();
                app.UseSwaggerUi();
            }
        }
    }
}