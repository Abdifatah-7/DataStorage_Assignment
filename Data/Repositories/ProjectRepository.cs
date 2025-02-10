using Data.Context;
using Data.Entities;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context)
{
    private readonly DataContext? _contex;
}

