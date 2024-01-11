using SASP.Client.Dtos;
using SASP.Client.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SASP.Client.DataServices
{
    public class SubscriptionDataService : IRestDataService<Subscription, SubscriptionDto>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public SubscriptionDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5110" : "https://localhost:7067";
            _url = $"{_baseAddress}/subscription";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task AddAsync(Subscription sub)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Нет доступа в интернет...");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize<Subscription>(sub, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Успешное создание Issue");
                }
                else
                {
                    Debug.WriteLine("Ответ не соответсвует http 2xx");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Упс, ошибка: {ex}");
            }

            return;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SubscriptionDto>> GetAllAsync()
        {
            List<SubscriptionDto> subs = new List<SubscriptionDto>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Нет доступа в интернет...");
                return subs;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    subs = JsonSerializer.Deserialize<List<SubscriptionDto>>(content, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("Ответ не соответсвует http 2xx");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Упс, ошибка: {ex}");
            }

            return subs.OrderBy(i => i.SubscriptionId).ToList();
        }

        public Task<SubscriptionDto> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Subscription item)
        {
            throw new NotImplementedException();
        }
    }
}
