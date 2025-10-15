using Demo.Shared.Entities;
using Demo.Shared.Repositories;
using DemoProject.DataAccess;
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

    public async Task<Snack?> GetAsync(int id)
    {
        return await _context.Snacks.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Snack> AddAsync(Snack newSnack)
    {
        _context.Snacks.Add(newSnack);
        await _context.SaveChangesAsync();
        return newSnack; // Id has been added
    }
}