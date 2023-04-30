using NoteTalking.Models;

namespace NoteTalking.ViewModels;

public class NoteViewModel : ViewModelBase
{
    private readonly Note _note;

    public NoteViewModel()
    {
            
    }
    public NoteViewModel(Note note)
    {
        _note = note;
    }

    public string? Text => _note.Text;

    public string Title => _note.Title;
}