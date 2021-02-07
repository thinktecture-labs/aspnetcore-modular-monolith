using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Monolith.Host
{
    /// <summary>
    /// Post configure options implementation so we can inject <seealso cref="Module"/> and pass it to
    /// <seealso cref="ModuleRoutingConvention"/>.
    /// </summary>
    public class ModuleRoutingMvcOptionsPostConfigure : IPostConfigureOptions<MvcOptions>
    {
        private readonly IEnumerable<Module> _modules;

        public ModuleRoutingMvcOptionsPostConfigure(IEnumerable<Module> modules)
        {
            _modules = modules;
        }

        public void PostConfigure(string name, MvcOptions options)
        {
            options.Conventions.Add(new ModuleRoutingConvention(_modules));
        }
    }
}