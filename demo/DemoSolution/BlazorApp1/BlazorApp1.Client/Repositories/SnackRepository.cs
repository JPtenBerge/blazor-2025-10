using Demo.Shared.Dtos;
using Demo.Shared.Entities;
using Demo.Shared.Repositories;
using Flurl.Http;

namespace BlazorApp1.Client.Repositories;

public class SnackRepository : ISnackRepository
{
    public async Task<IEnumerable<Snack>> GetAllAsync()
    {
        var response = await "https://localhost:5002/api/snacks".GetJsonAsync<SnackGetAllResponseDto>();
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

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await $"https://localhost:5002/api/snacks/{id}".DeleteAsync().ReceiveString();
        return result == "true";
    }
}