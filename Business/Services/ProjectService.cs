namespace Business.Services;

using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Repositories;




public class ProjectService(ProjectRepository projectRepository) : IProjectService
{
    private readonly ProjectRepository _projectRepository = projectRepository;

    // Skapa ett nytt projekt
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var projectEntity = ProjectFactory.Create(form);
        if (projectEntity == null)
            return false;

        await _projectRepository.CreateAsync(projectEntity);
        return true;
    }

    // Hämta alla projekt
    public async Task<IEnumerable<Project?>> GetProjectsAsync()
    {
        var projectEntities = await _projectRepository.GetAllAsync();
        return projectEntities.Select(ProjectFactory.Create)!;
    }

    // Hämta projekt via ID
    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        var projectEntity = await _projectRepository.GetAsync(p => p.Id == id);
        return ProjectFactory.Create(projectEntity);
    }

    // Uppdatera ett projekt
    public async Task<bool> UpdateProjectAsync(Project project)
    {
        try
        {
            var existingProject = await _projectRepository.GetAsync(p => p.Id == project.Id);
            if (existingProject == null)
                return false;

            ProjectFactory.UpdateEntity(existingProject, project);

            var result = await _projectRepository.UpdateAsync(p => p.Id == project.Id, existingProject);
            return result != null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // Ta bort ett projekt
    public async Task<bool> DeleteProjectAsync(int id)
    {
        try
        {
            var projectEntity = await _projectRepository.GetAsync(p => p.Id == id);
            if (projectEntity == null)
                return false;

            return await _projectRepository.DeleteAsync(p => p.Id == id);
        }
        catch (Exception)
        {
            return false;
        }
    }
}

