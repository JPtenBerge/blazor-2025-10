using Demo.Shared.Entities;

namespace Demo.Shared.Repositories;

public interface ISnackRepository
{
	Task<IEnumerable<Snack>> GetAllAsync();
	Task<Snack?> GetAsync(int id);
	Task<Snack> AddAsync(Snack newSnack);
	Task<bool> DeleteAsync(int id);
}
