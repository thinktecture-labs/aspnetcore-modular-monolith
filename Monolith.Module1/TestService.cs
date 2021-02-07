using Monolith.Module1.Shared;

namespace Monolith.Module1
{
    internal class TestService : ITestService
    {
        public string SayHello()
        {
            return "Hello world from Test Service (Module1)";
        }
    }
}