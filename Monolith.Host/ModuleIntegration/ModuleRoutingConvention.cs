using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Monolith.Host
{
    /// <summary>
    /// Adds the route prefix to all actions 
    /// </summary>
    public class ModuleRoutingConvention : IActionModelConvention
    {
        private readonly IEnumerable<Module> _modules;

        public ModuleRoutingConvention(IEnumerable<Module> modules)
        {
            _modules = modules;
        }

        public void Apply(ActionModel action)
        {
            var module = _modules.FirstOrDefault(m => m.Assembly == action.Controller.ControllerType.Assembly);
            if (module == null)
            {
                return;
            }

            action.RouteValues.Add("module", module.RoutePrefix);
        }
    }
}