using DotNet_Console_Hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Console_Hotel.Infrastructure;

internal class HotelBookerContext : DbContext
{
    private readonly string _connectionString;

    public HotelBookerContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Room> Rooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().HasKey(c => c.Id);
        modelBuilder.Entity<Reservation>().HasKey(r => r.Id);
        modelBuilder.Entity<Hotel>().HasKey(h => h.Id);
        modelBuilder.Entity<Room>().HasKey(r => r.Id);
        
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // table in lower
            entity.SetTableName(entity.GetTableName()!.ToLower());

            // all prop in lower
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.Name.ToLower());
            }
        }
    }
}