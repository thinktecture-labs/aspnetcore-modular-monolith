using Microsoft.AspNetCore.Mvc;

namespace Monolith.Module2
{
    [Route("[module]/[controller]")]
    internal class TestController : Controller
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "Hello World from TestController in Module 2";
        }
    }
}