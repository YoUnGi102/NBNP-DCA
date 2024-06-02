using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using WebAPI;
using WebAPI.Endpoints.ViaEvents;
using Xunit;

namespace IntegrationTests.WebAPI.Event;

public class CreateEventTests
{
    private readonly WebApplicationFactory<Program> _factory;

    public CreateEventTests()
    {
        _factory = new VeaWebApplicationFactory();
    }
    
    [Fact]
    public async Task CreateEventTest()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        string requestUri = "http://localhost:5298/api/events/create";
        CreateEventRequest request = new("Test Event", "Test Description", "2022-12-12", "2022-12-12", 10, "Public",
            "Active", Guid.NewGuid());
        StringContent content = new(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        // Act
        HttpResponseMessage response = await client.PostAsync(requestUri, content);
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        CreateEventResponse responseObj = JsonConvert.DeserializeObject<CreateEventResponse>(responseString);

        // Assert
        Assert.NotNull(responseObj);
        Assert.NotEqual(Guid.Empty.ToString(), responseObj.Id);
    }
}