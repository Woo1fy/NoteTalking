using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteTalking.Models;
using NoteTalking.Repositories;

namespace NoteTalking.Services;

public interface INoteService
{
	Task<List<NoteEntity>> SearchAsync(string searchTerm);
}

public class NoteService : INoteService
{
    private ApplicationContext DbContext { get; init; }
    private INoteRepository NoteRepository { get; init; }
    public NoteService(ApplicationContext dbContext, INoteRepository noteRepository)
    {
        DbContext = dbContext;
        NoteRepository = noteRepository;
    }

    
}