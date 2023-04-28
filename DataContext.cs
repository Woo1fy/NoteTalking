using Microsoft.EntityFrameworkCore;
using NoteTalking.Models;

namespace NoteTalking;

public sealed class DataContext : DbContext
{ 
    public DbSet<NoteItem> Notes { get; set; } = null!;

    public DataContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=noteTalking.db");
    }
    
    
}