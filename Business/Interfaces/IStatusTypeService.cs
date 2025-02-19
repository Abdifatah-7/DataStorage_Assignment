using Business.Models;

namespace Business.Interfaces
{
    public interface IStatusTypeService
    {
        Task<bool> CreateStatusTypeAsync(StatusTypeRegistration form);
        Task<bool> DeleteStatusTypeAsync(int id);
        Task<StatusType?> GetStatusTypeByIdAsync(int id);
        Task<IEnumerable<StatusType?>> GetStatusTypesAsync();
        Task<bool> UpdateStatusTypeAsync(StatusType statusType);
    }
}