using Business.Helpers;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectEntity? Create(ProjectRegistrationForm form)
    {
        if (form == null) return null;

        return new ProjectEntity
        {
            ProjectName = form.ProjectName,
            ProjectNumber = ProjectNumberGenerator.GenerateProjectNumber(), 
            TotalPrice = form.TotalPrice,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            CustomerId = form.CustomerId,
            ProductId = form.ProductId,
            UserId = form.UserId,
            StatusTypeId = form.StatusTypeId
        };
    }

    public static Project? Create(ProjectEntity entity)
    {
        if (entity == null) return null;

        return new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            ProjectNumber = entity.ProjectNumber,
            TotalPrice = entity.TotalPrice,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CustomerId = entity.CustomerId,
            ProductId = entity.ProductId,
            UserId = entity.UserId,
            StatusTypeId = entity.StatusTypeId
        };
    }

    public static void UpdateEntity(ProjectEntity existingEntity, Project updatedProject)
    {
        existingEntity.ProjectName = updatedProject.ProjectName;
        existingEntity.TotalPrice = updatedProject.TotalPrice;
        existingEntity.StartDate = updatedProject.StartDate;
        existingEntity.EndDate = updatedProject.EndDate;
        existingEntity.CustomerId = updatedProject.CustomerId;
        existingEntity.ProductId = updatedProject.ProductId;
        existingEntity.UserId = updatedProject.UserId;
        existingEntity.StatusTypeId = updatedProject.StatusTypeId;
    }

}

