using Microsoft.EntityFrameworkCore;
namespace DotNet_Api.Models;

public class MyDbContext : DbContext
{
    public MyDbContext() : base()
    {

    }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {

    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {

    //}

    public DbSet<Product> Products { get; set; }
}

