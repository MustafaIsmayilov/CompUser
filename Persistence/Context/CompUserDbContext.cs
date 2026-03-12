using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class CompUserDbContext : IdentityDbContext<AppUser>
{
    public CompUserDbContext(DbContextOptions<CompUserDbContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(CompUserDbContext).Assembly);
    }
}
