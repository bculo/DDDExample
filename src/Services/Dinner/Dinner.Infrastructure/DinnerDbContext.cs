using Dinner.Domain.Menu;
using Microsoft.EntityFrameworkCore;

namespace Dinner.Infrastructure;

public class DinnerDbContext : DbContext
{
    public virtual DbSet<Menu> Menus { get; set; }

    public DinnerDbContext(DbContextOptions<DinnerDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DinnerDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
