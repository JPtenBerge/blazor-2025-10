using Demo.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DataAccess;

public class SnackContext : DbContext
{
	public DbSet<Snack> Snacks { get; set; }
	
	public SnackContext(DbContextOptions<SnackContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Snack>().HasKey(x => x.Id);
		modelBuilder.Entity<Snack>().Property(x => x.Name).IsRequired().HasMaxLength(200);
		modelBuilder.Entity<Snack>().Property(x => x.PhotoUrl).IsRequired().HasMaxLength(500);
		modelBuilder.Entity<Snack>().Property(x => x.Rating).HasPrecision(2, 1);
	}
}