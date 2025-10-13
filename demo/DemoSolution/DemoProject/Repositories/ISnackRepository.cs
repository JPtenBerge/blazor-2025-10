using DemoProject.Entities;

namespace DemoProject.Repositories;

public interface ISnackRepository
{
	Task<IEnumerable<Snack>> GetAllAsync();
	Task<Snack> AddAsync(Snack newSnack);
}