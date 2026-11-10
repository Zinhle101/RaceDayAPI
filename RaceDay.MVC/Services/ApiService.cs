
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RaceDay.MVC.Services
{
    // This class allows MVC to communicate with the Part 2 API.
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        // Sends a GET request to the API.
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var request = CreateRequest(HttpMethod.Get, url);

            return await _httpClient.SendAsync(request);
        }

        // Sends a POST request to the API.
        public async Task<HttpResponseMessage> PostAsync<T>(
            string url, T data)
        {
            var request = CreateRequest(HttpMethod.Post, url);

            request.Content = JsonContent.Create(data);

            return await _httpClient.SendAsync(request);
        }

        // Sends a PUT request to the API.
        public async Task<HttpResponseMessage> PutAsync<T>(
            string url, T data)
        {
            var request = CreateRequest(HttpMethod.Put, url);

            request.Content = JsonContent.Create(data);

            return await _httpClient.SendAsync(request);
        }

        // Sends a DELETE request to the API.
        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var request = CreateRequest(HttpMethod.Delete, url);

            return await _httpClient.SendAsync(request);
        }

        // Creates requests and attaches the user's JWT token when available.
        private HttpRequestMessage CreateRequest(
            HttpMethod method, string url)
        {
            var request = new HttpRequestMessage(method, url);

            var token = _httpContextAccessor.HttpContext?
                .Session.GetString("ApiToken");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return request;
        }
    }
}
