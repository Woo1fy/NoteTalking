using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NoteTalking.Models;

namespace NoteTalking;

public class ApplicationContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Note>()
            .HasIndex(f => f.Order)
            .IsUnique();
        
        modelBuilder.Entity<NoteContent>().ToTable(nameof(NoteContent));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        
        var config = builder.Build();
        var connectionString = config.GetConnectionString("DefaultConnection");
        
        optionsBuilder
            .UseSqlite(connectionString);
    }
    
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<NoteContent> NoteContents => Set<NoteContent>();
}