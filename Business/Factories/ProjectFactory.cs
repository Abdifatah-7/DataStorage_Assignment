using Business.Helpers;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectEntity? Create(ProjectRegistrationForm form) => form == null ? null : new()
    {

        ProjectName = form.ProjectName,
        ProjectNumber = ProjectNumberGenerator.GenerateProjectNumber(),
        Description = form.Description,
        TotalPrice = form.TotalPrice,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        CustomerId = form.CustomerId,
        ProductId = form.ProductId,
        UserId = form.UserId,
        StatusId = form.StatusId

    };

    public static Project? Create(ProjectEntity Entities) => Entities == null ? null : new()
    {

        Id = Entities.Id,
        ProjectName = Entities.ProjectName,
        ProjectNumber = Entities.ProjectNumber,
        Description = Entities.Description,
        TotalPrice = Entities.TotalPrice,
        StartDate = Entities.StartDate,
        EndDate = Entities.EndDate,
        CustomerId = Entities.CustomerId,
        ProductId = Entities.ProductId,
        UserId = Entities.UserId,
        StatusId = Entities.StatusId

    };

    public static void UpdateEntity(ProjectEntity existingEntity, Project updatedProject)
    {
        existingEntity.ProjectName = updatedProject.ProjectName;
        existingEntity.TotalPrice = updatedProject.TotalPrice;
        existingEntity.Description = updatedProject.Description;
        existingEntity.StartDate = updatedProject.StartDate;
        existingEntity.EndDate = updatedProject.EndDate;
        existingEntity.CustomerId = updatedProject.CustomerId;
        existingEntity.ProductId = updatedProject.ProductId;
        existingEntity.UserId = updatedProject.UserId;
        existingEntity.StatusId = updatedProject.StatusId;
    }

}

