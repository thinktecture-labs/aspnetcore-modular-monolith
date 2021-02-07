using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Monolith.Host
{
    /// <summary>
    /// Custom feature provider to allow "internal" controllers
    /// </summary>
    public class InternalControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            var isCustomController = !typeInfo.IsAbstract && typeof(ControllerBase).IsAssignableFrom(typeInfo);
            return isCustomController || base.IsController(typeInfo);
        }
    }
}