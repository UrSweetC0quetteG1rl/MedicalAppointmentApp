using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MedicalAppointment.Web.Controllers
{
    public abstract class BaseApiController : Controller
    {
        private readonly string _baseUrl;

        protected BaseApiController(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        protected async Task<T> SendHttpRequestAsync<T>(string endpoint, HttpMethod method, object? data = null)
        {
            using var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
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
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        protected IActionResult HandleError(Exception ex, string redirectAction = "Index")
        {
            ViewBag.Message = ex.Message;
            return RedirectToAction(redirectAction);
        }
    }
}
