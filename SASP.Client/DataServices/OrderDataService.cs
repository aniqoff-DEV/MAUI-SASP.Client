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
    public class OrderDataService : IOrderRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public OrderDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5110" : "https://localhost:7067";
            _url = $"{_baseAddress}/orderhistory";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public Task AddAsync(Order item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            List<OrderDto> orders = new List<OrderDto>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Нет доступа в интернет...");
                return orders;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    orders = JsonSerializer.Deserialize<List<OrderDto>>(content, _jsonSerializerOptions);
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

            return orders.OrderBy(o => o.OrderId).ToList();
        }

        public Task<OrderDto> GetAsync(int idItem)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetRowAsync(int idItem)
        {
            Order order = new();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Нет доступа в интернет...");
                return order;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/row/{idItem}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    order = JsonSerializer.Deserialize<Order>(content, _jsonSerializerOptions);
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

            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Нет доступа в интернет...");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize<Order>(order, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PatchAsync($"{_url}/{order.OrderId}", content);

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
    }
}
