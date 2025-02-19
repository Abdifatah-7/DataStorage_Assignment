using Business.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(ProjectRegistrationForm form);
        Task<bool> DeleteProjectAsync(int id);
        Task<Project?> GetProjectByIdAsync(int id);
        Task<IEnumerable<Project?>> GetProjectsAsync();
        Task<bool> UpdateProjectAsync(Project project);
    }
}