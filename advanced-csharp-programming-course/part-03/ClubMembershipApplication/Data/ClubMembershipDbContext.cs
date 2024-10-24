using ClubMembershipApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubMembershipApplication.Data;

public class ClubMembershipDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}ClubMembershipDb.db");
        base.OnConfiguring(optionsBuilder);
    }
}