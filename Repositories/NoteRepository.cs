using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteTalking.Models;

namespace NoteTalking.Repositories;

public interface INoteRepository
{
    Task<NoteEntity> CreateAsync(NoteEntity note);
    Task<List<NoteEntity>> ReadAsync();
    Task<NoteEntity> ReadAsync(long noteEntityId);
    Task<NoteEntity> UpdateAsync(NoteEntity note);
    Task<bool> DeleteAsync(long noteEntityId);
    Task<bool> DeleteAsync(NoteEntity note);
    Task<List<NoteEntity>> SearchAsync(string searchTerm);
}

public class NoteRepository : INoteRepository
{
    private ApplicationContext DbContext { get; init; }

    public NoteRepository(ApplicationContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<NoteEntity> CreateAsync(NoteEntity note)
    {
        await DbContext.Notes.AddAsync(note);

        await DbContext.SaveChangesAsync();

        return note;
    }

    public async Task<List<NoteEntity>> ReadAsync()
    {
        return await DbContext.Notes
            .OrderByDescending(v => v.Id)
            .ToListAsync();
    }

    public async Task<NoteEntity> ReadAsync(long noteEntityId)
    {
        return await DbContext.Notes
            .Where(v => v.Id == noteEntityId)
            .SingleAsync();
    }

    public async Task<NoteEntity> UpdateAsync(NoteEntity note)
    {
        DbContext.Notes.Attach(note);

        await DbContext.SaveChangesAsync();

        return note;
    }

    public async Task<bool> DeleteAsync(long noteEntityId)
    {
        NoteEntity note = await DbContext.Notes
            .SingleAsync(t => t.Id == noteEntityId);

        DbContext.Remove(note);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(NoteEntity note)
    {
        return await DeleteAsync(note.Id);
    }
    
    public async Task<List<NoteEntity>> SearchAsync(string searchTerm)
    {
        return await DbContext.Notes.Where(
            n => !string.IsNullOrEmpty(n.Text)
                 && (n.Text.Contains(searchTerm)
                     || n.Title.Contains(searchTerm))).ToListAsync();
    }
}