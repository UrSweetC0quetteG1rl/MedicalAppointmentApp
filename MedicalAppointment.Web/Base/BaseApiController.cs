using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public abstract class BaseApiController : Controller
{
    // Define la URL base aquí
    protected readonly string BaseUrl = "http://localhost:5138/api/";

    private readonly IHttpClientFactory _httpClientFactory;

    protected BaseApiController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // Método para enviar solicitudes HTTP
    protected async Task<T?> SendHttpRequestAsync<T>(string endpoint, HttpMethod method, object? data = null) where T : class
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(BaseUrl); // Usa la URL base

        var request = new HttpRequestMessage(method, endpoint);

        if (data != null)
        {
            request.Content = JsonContent.Create(data);
        }

        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            string errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error: {response.StatusCode}, Details: {errorMessage}");
        }

        string responseContent = await response.Content.ReadAsStringAsync();

        

        try
        {
            return JsonConvert.DeserializeObject<T>(responseContent);
        }
        catch (JsonException ex)
        {
            throw new Exception("Error deserializing the response content.", ex);
        }
    }
}
