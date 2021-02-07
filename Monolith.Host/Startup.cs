using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Monolith.Host
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Modular Monolith"
                });
            });

            services.AddControllers().ConfigureApplicationPartManager(manager =>
            {
                // Clear all auto detected controllers.
                manager.ApplicationParts.Clear();

                // Add feature provider to allow "internal" controller
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

            // Register a convention allowing to us to prefix routes to modules.
            services.AddTransient<IPostConfigureOptions<MvcOptions>, ModuleRoutingMvcOptionsPostConfigure>();

            // Adds module1 with the route prefix module-1
            services.AddModule<Module1.Startup>("module-1");

            // Adds module2 with the route prefix module-2
            services.AddModule<Module2.Startup>("module-2");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // Adds endpoints defined in modules
            var modules = app.ApplicationServices.GetRequiredService<IEnumerable<Module>>();
            foreach (var module in modules)
            {
                app.Map($"/{module.RoutePrefix}", builder =>
                {
                    builder.UseRouting();
                    module.Startup.Configure(builder, env);
                });
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = String.Empty;
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Modular Monolith V1");
            });
        }
    }
}