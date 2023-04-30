using System.Threading.Tasks;
using System.Windows.Input;
using NoteTalking.Models;
using ReactiveUI;

namespace NoteTalking.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
	private string? _searchText;
	public string? SearchText
	{
		get => _searchText;
		set => this.RaiseAndSetIfChanged(ref _searchText, value);
	}
	
	private ViewModelBase _content = null!;
    
	public ViewModelBase Content
	{
		get => _content;
		private set => this.RaiseAndSetIfChanged(ref _content, value);
	}

	public MainWindowViewModel(ApplicationContext dbContext)
	{
		Content = new NotesTabViewModel(dbContext, this);
		
		ShowNotesTab = ReactiveCommand.Create(() => Content = new NotesTabViewModel(dbContext, this));
		ShowRemindersTab = ReactiveCommand.Create(() => Content = new RemindersTabViewModel());
		ShowLabelingTab = ReactiveCommand.Create(() => Content = new LabelingTabViewModel());
		ShowArchiveTab = ReactiveCommand.Create(() => Content = new ArchiveTabViewModel());
		ShowTrashTab = ReactiveCommand.Create(() => Content = new TrashTabViewModel());

		AddNoteCmd = ReactiveCommand.CreateFromTask(async () =>
		{
			await AddNote(dbContext);
		});
	}

	#region ICommand Tabs Implementations

	public ICommand? ShowNotesTab { get; }
	public ICommand? ShowRemindersTab{ get; }
	public ICommand? ShowLabelingTab{ get; }
	public ICommand? ShowArchiveTab{ get; }
	public ICommand? ShowTrashTab{ get; }
	
	#endregion
	
	public ICommand AddNoteCmd { get; }

	private static async Task AddNote (ApplicationContext context)
	{
		var note = new Note
		{
			Title = "test4"
		};
		await context.Notes.AddAsync(note);
		await context.SaveChangesAsync();
	}
}
