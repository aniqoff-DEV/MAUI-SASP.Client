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
    public class IssueDataService : IRestDataService<Issue, IssueDto>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public IssueDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5110" : "https://localhost:7067";
            _url = $"{_baseAddress}/issue";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task AddAsync(Issue issue)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Нет доступа в интернет...");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize<Issue>(issue, _jsonSerializerOptions);
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

        public async Task<List<IssueDto>> GetAllAsync()
        {
            List<IssueDto> issues = new List<IssueDto>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Нет доступа в интернет...");
                return issues;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    issues = JsonSerializer.Deserialize<List<IssueDto>>(content, _jsonSerializerOptions);
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

            return issues.OrderBy(i => i.Title).ToList();
        }

        public async Task<IssueDto> GetAsync(int id)
        {
            var issue = new IssueDto();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Нет доступа в интернет...");
                return issue;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    issue = JsonSerializer.Deserialize<IssueDto>(content, _jsonSerializerOptions);
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

            return issue;
        }

        public Task UpdateAsync(Issue item)
        {
            throw new NotImplementedException();
        }
    }
}
