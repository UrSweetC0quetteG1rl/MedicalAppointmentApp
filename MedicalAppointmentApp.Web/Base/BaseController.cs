
using Azure;
using MedicalAppointmentApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace MedicalAppointmentApp.Web.Base
{
    public  abstract class BaseController<TDto, TGetAllResultModel, TGetByIdModel> : Controller
    {
        protected readonly HttpClient _client;
        protected readonly string _baseUrl;

        protected BaseController(string baseUrl)
        {
            _baseUrl = baseUrl;
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        protected async Task<TGetAllResultModel> GetAllAsync(string endpoint)
        {
            var responseTask = await _client.GetAsync(endpoint);
            if (responseTask.IsSuccessStatusCode)
            {
                string response = await responseTask.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TGetAllResultModel>(response);
            }
            return default;
        }

        protected async Task<TGetByIdModel> GetByIdAsync(string endpoint, int id)
        {
            var responseTask = await _client.GetAsync($"{endpoint}?id={id}");
            if (responseTask.IsSuccessStatusCode)
            {
                string response = await responseTask.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TGetByIdModel>(response);
            }
            return default;
        }

        protected async Task<BaseApiResponseModel> CreateAsync(string endpoint, TDto dto)
        {
            try
            {
                var responseTask = await _client.PostAsJsonAsync(endpoint, dto);

                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<BaseApiResponseModel>(response);
                }
                else
                {
                    string errorContent = await responseTask.Content.ReadAsStringAsync();
                    return new BaseApiResponseModel
                    {
                        IsSuccess = false,
                        message = $"Error en la creación: {errorContent}"
                    };
                }
            }
            catch (Exception ex) 
            {
                return new BaseApiResponseModel
                {
                    IsSuccess = false,
                    message = $"Excepción durante la creación: {ex.Message}"
                };
            }
        }

        protected async Task<BaseApiResponseModel> UpdateAsync(string endpoint, int id, TDto dto)
        {
            var responseTask = await _client.PutAsJsonAsync($"{endpoint}/{id}", dto);
            if (responseTask.IsSuccessStatusCode)
            {
                string response = await responseTask.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BaseApiResponseModel>(response);
            }
            return new BaseApiResponseModel { IsSuccess = false, message = "Error en la actualización." };
        }


    }
}
