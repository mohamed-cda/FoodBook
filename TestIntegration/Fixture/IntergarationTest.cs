using API;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;

namespace TestIntegration
{
    public abstract class IntergrationTest: IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly WebApplicationFactory<Startup> _factory;
       protected readonly HttpClient _client;


        public IntergrationTest(WebApplicationFactory<Startup>factory)
        {
            _factory = factory;
           _client= factory.CreateClient();
        }
    }
}
