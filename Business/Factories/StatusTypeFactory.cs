using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class StatusTypeFactory
{
    public static StatusTypeEntity? Create(StatusTypeRegistration form) => form == null ? null : new()
    {
        StatusName = form.StatusName
    };

    public static StatusType? Create(StatusTypeEntity entity) => entity == null ? null : new()
    {
        Id = entity.Id,
        StatusName = entity.StatusName
    };

    public static void UpdateEntity(StatusTypeEntity existingEntity, StatusType updatedStatusType)
    {
        existingEntity.StatusName = updatedStatusType.StatusName;
    }
}
