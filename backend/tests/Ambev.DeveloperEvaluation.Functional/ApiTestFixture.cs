using Microsoft.AspNetCore.Mvc.Testing;

namespace Ambev.DeveloperEvaluation.Functional
{
    public class ApiTestFixture : WebApplicationFactory<Ambev.DeveloperEvaluation.WebApi.Program>
    {
        public HttpClient Client { get; }

        public ApiTestFixture()
        {
            Client = CreateClient();
        }
    }
}
