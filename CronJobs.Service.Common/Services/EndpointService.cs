using CronJobs.Service.Common.Interface;
using CronJobs.Service.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CronJobs.Service.Common.Services
{
    public class EndpointService : IEndpointService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationUtilityManager _configureUtilityManager;

        public EndpointService(HttpClient httpClient, IConfigurationUtilityManager configureUtilityManager)
        {
            _configureUtilityManager = configureUtilityManager;
            httpClient.BaseAddress = new Uri(_configureUtilityManager.EndPoint);
            _httpClient = httpClient;

        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(ApiRequest apiRequest)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                var apiUrl = GetApiUri(apiRequest.ApiUrl);
                responseMessage = await _httpClient.GetAsync(apiUrl).ConfigureAwait(false);

                if (!responseMessage.IsSuccessStatusCode)
                {
                    var errorContent = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    throw new Exception($"API {apiRequest.ApiUrl} returned status code {responseMessage.StatusCode} with message {errorContent}");
                }

                // Deserialize using Newtonsoft.Json as an alternative
                var jsonString = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EndpointService > GetAsync failed: {ex.Message}");
                throw;
            }
        }

        public async Task<T> PostAsync<T>(ApiRequest apiRequest)
        {
            var dataAsString = JsonConvert.SerializeObject(apiRequest.Data);
            using var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage responseMessage = null;

            try
            {
                responseMessage = await _httpClient.PostAsync(GetApiUri(apiRequest.ApiUrl), content).ConfigureAwait(false);

                if (!responseMessage.IsSuccessStatusCode)
                {
                    var errorContent = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    throw new Exception($"API {apiRequest.ApiUrl} returned status code {responseMessage.StatusCode} with message {errorContent}");
                }

                // Deserialize using Newtonsoft.Json as an alternative
                var jsonString = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EndpointService > PostAsync failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Get full api uri
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        private string GetApiUri(string relativePath)
        {
            return _httpClient.BaseAddress + relativePath;
        }
    }
}
