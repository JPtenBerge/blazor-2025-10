using Demo.Shared.Entities;
using Demo.Shared.Repositories;

namespace BlazorApp1.Client;

public class SnackRepository : ISnackRepository
{
    public Task<IEnumerable<Snack>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Snack?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Snack> AddAsync(Snack newSnack)
    {
        throw new NotImplementedException();
    }
}