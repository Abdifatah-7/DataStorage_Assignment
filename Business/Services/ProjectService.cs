namespace Business.Services;

using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;





public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    // Skapa ett nytt projekt
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {

        try
        {
            var existingProject = await _projectRepository.GetAsync(p => p.ProjectName == form.ProjectName);
            if (existingProject != null)
                return false;

            var projectEntity = ProjectFactory.Create(form);
            if (projectEntity == null)
                return false;

            var result = await _projectRepository.CreateAsync(projectEntity);
            return result != null;
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Error creating project: {ex.Message}");
            return false;
        }
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

