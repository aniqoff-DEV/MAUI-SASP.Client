using SASP.Client.Dtos;
using SASP.Client.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SASP.Client.DataServices
{
    public class UserDataService : IRestDataService<User, UserDto>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public UserDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5110" : "https://localhost:7067";
            _url = $"{_baseAddress}/user";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public Task AddAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            List<UserDto> users = new List<UserDto>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Нет доступа в интернет...");
                return users;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    users = JsonSerializer.Deserialize<List<UserDto>>(content, _jsonSerializerOptions);
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

            return users.OrderBy(i => i.Name).ToList();
        }

        public Task<UserDto> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User item)
        {
            throw new NotImplementedException();
        }
    }
}
