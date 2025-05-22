using SkillSnap.Api.Models;

namespace SkillSnap.Front.Services
{

    public class SkillService
    {
        private readonly HttpClient _httpClient;

        public SkillService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Skill>> GetSkillsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Skill>>("api/Skills");
        }

        public async Task<Skill?> GetSkillByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Skill>($"api/Skills/{id}");
        }

        public async Task<Skill?> CreateSkillAsync(Skill skill)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Skills", skill);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Skill>();
            }
            return null;
        }

        public async Task<bool> UpdateSkillAsync(int id, Skill skill)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Skills/{id}", skill);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteSkillAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Skills/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
