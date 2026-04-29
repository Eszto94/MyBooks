using MyBooks.FrontendWASM.Models;
using System.Net.Http.Json;

namespace MyBooks.FrontendWASM.Services
{
    public class UserSession
    {
        private readonly HttpClient _httpClient;

        private User? user;

        public UserSession(HttpClient client)
        {
            _httpClient = client;
        }

        public string? UserId => user?.Id;

        public string? Nickname => user?.Name;

        public bool IsLoggedIn => !string.IsNullOrWhiteSpace(Nickname);

        public async Task Login(string nickname)
        {
            nickname = nickname.Trim();

            var response = await _httpClient.GetAsync("users/by-name/" + nickname);

            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadFromJsonAsync<User>();
            }
            else
            {
                var createResponse = await _httpClient.PostAsJsonAsync("users", new User { Name = nickname });
                createResponse.EnsureSuccessStatusCode();

                user = await _httpClient.GetFromJsonAsync<User>("users/by-name/" + nickname);
            }

            OnChange?.Invoke();
        }

        public void Logout()
        {
            user = null;

            OnChange?.Invoke();
        }

        public event Action? OnChange;
    }
}
