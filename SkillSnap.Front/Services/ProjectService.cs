using SkillSnap.Api.Models;

namespace SkillSnap.Front.Services
{

    public class ProjectService
    {
        private readonly HttpClient _httpClient;

        public ProjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Project>>("api/Projects");
        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Project>($"api/Projects/{id}");
        }

        public async Task<Project?> CreateProjectAsync(Project project)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Projects", project);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Project>();
            }
            return null;
        }

        public async Task<bool> UpdateProjectAsync(int id, Project project)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Projects/{id}", project);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Projects/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
