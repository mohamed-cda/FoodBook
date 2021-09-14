using API;
using BO.Entity;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestIntegration.Fixture
{
    public class RestaurantControllerIntegrationTest:IntergrationTest
    {
        public RestaurantControllerIntegrationTest(WebApplicationFactory<Startup> factory) : base(factory) { }
        Ingredient test = new Ingredient();
        [Fact]
        public async Task GetMenuById_should_Be_Ok()
        {
            

            // Arrange
            Plat expected = new Plat()
            {
                Id_plat = 58,
                Nom = "Boulettes de viande à la sauce tomate ",
                Id_Type = 1,
                Type = null,
                Ingredient = new List<Ingredient>() { test }
        };
            // Act
            try
            {
                var response = await _client.GetFromJsonAsync<Plat>("api/v1/plat/58");
                // Assert
                Assert.Equal(response, expected);
            }
            catch
            {
                Assert.True(false);
            }
           
        }
    }
}
