using Demo.Shared.Dtos;
using Demo.Shared.Entities;
using Demo.Shared.Repositories;
using Flurl.Http;

namespace BlazorApp1.Client.Repositories;

public class SnackRepository : ISnackRepository
{
    public async Task<IEnumerable<Snack>> GetAllAsync()
    {
        var response = await "http://localhost:5002/api/snacks".GetJsonAsync<SnackGetAllResponseDto>();
        return response.Snacks.Select(s => s.ToEntity());
    }

    public async Task<Snack?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Snack> AddAsync(Snack newSnack)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}