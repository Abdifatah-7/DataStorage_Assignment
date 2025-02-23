using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;


namespace Business.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;


    public async Task<bool> CreateStatusTypeAsync(StatusTypeRegistration form)
    {
        var existingStatus = await _statusTypeRepository.GetAsync(s => s.StatusName == form.StatusName);
        if (existingStatus != null)
            return false;

        var statusEntity = StatusTypeFactory.Create(form);
        await _statusTypeRepository.CreateAsync(statusEntity!);
        return true;
    }


    public async Task<IEnumerable<StatusType?>> GetStatusTypesAsync()
    {
        var statusEntities = await _statusTypeRepository.GetAllAsync();
        return statusEntities.Select(StatusTypeFactory.Create)!;
    }


    public async Task<StatusType?> GetStatusTypeByIdAsync(int id)
    {
        var statusEntity = await _statusTypeRepository.GetAsync(s => s.Id == id);
        return StatusTypeFactory.Create(statusEntity!);
    }


    public async Task<bool> UpdateStatusTypeAsync(StatusType statusType)
    {
        try
        {
            var existingStatus = await _statusTypeRepository.GetAsync(s => s.Id == statusType.Id);
            if (existingStatus == null)
                return false;

            StatusTypeFactory.UpdateEntity(existingStatus, statusType);

            var result = await _statusTypeRepository.UpdateAsync(s => s.Id == statusType.Id, existingStatus);
            return result != null;
        }
        catch (Exception)
        {
            return false;
        }
    }


    public async Task<bool> DeleteStatusTypeAsync(int id)
    {
        try
        {
            var statusEntity = await _statusTypeRepository.GetAsync(s => s.Id == id);
            if (statusEntity == null)
                return false;

            return await _statusTypeRepository.DeleteAsync(s => s.Id == id);
        }
        catch (Exception)
        {
            return false;
        }
    }
}
