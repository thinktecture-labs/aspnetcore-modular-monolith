using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Monolith.Module1.Shared;
using IStartup = Monolith.Shared.IStartup;

namespace Monolith.Module1
{
    public class Startup : IStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITestService, TestService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
                endpoints.MapGet("/TestEndpoint",
                    async context =>
                    {
                        await context.Response.WriteAsync("Hello World from TestEndpoint in Module 1");
                    }).RequireAuthorization()
            );
        }
    }
}