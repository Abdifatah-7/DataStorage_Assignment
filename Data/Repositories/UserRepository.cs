using Data.Context;
using Data.Entities;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context)
{
    private readonly DataContext? _contex;
}

