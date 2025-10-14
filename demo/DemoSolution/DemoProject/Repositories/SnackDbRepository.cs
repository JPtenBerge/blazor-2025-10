using DemoProject.DataAccess;
using DemoProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Repositories;

public class SnackDbRepository : ISnackRepository
{
	private readonly SnackContext _context;

	public SnackDbRepository(SnackContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Snack>> GetAllAsync()
	{
		return await _context.Snacks.ToArrayAsync();
	}

	public async Task<Snack> AddAsync(Snack newSnack)
	{
		_context.Snacks.Add(newSnack);
		await _context.SaveChangesAsync();
		return newSnack; // Id has been added
	}
}