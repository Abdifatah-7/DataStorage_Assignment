using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class UserFactory
{
    public static UserEntity? Create(UserRegistrationForm form) => form == null ? null : new()
    {
        FirstName = form.FirstName,
        LastName = form.LastName,
        Email = form.Email,
    };

    public static User? Create(UserEntity entity) => entity == null ? null : new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Email = entity.Email,
    };

    public static void UpdateEntity(UserEntity existingEntity, User updatedUser)
    {
        existingEntity.FirstName = updatedUser.FirstName;
        existingEntity.FirstName = updatedUser.LastName;
        existingEntity.Email = updatedUser.Email;
    }
}
