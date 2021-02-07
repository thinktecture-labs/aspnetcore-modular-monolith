using Microsoft.AspNetCore.Mvc;
using Monolith.Module1.Shared;

namespace Monolith.Module1
{
    [Route("[module]/[controller]")]
    internal class TestController : Controller
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public ActionResult<string> Index()
        {
            return $"{_testService.SayHello()} in TestController in Module 1";
        }
    }
}