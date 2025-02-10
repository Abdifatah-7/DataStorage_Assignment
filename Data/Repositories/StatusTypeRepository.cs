using Data.Context;
using Data.Entities;

namespace Data.Repositories;

public class StatusTypeRepository(DataContext context) : BaseRepository<StatusTypeEntity>(context)
{
    private readonly DataContext? _contex;
}
