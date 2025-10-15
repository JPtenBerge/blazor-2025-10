using Demo.Shared.Entities;

namespace Demo.Shared.Repositories;

public interface ISnackRepository
{
	Task<IEnumerable<Snack>> GetAllAsync();
	Task<Snack> AddAsync(Snack newSnack);
}