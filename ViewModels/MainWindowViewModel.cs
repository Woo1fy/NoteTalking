using ReactiveUI;

namespace NoteTalking.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
	private readonly DataContext _context;
	public MainWindowViewModel(DataContext context)
	{
		_context = context;
	}

	private ViewModelBase _content = null!;
    
	public ViewModelBase Content
	{
		get => _content;
		private set => this.RaiseAndSetIfChanged(ref _content, value);
	}

	public void ShowNotesTab()
    {
		var vm = new NotesTabViewModel(_context);

		Content = vm;
	}

	public void ShowRemindersTab()
	{
		var vm = new RemindersTabViewModel();

		Content = vm;
	}

	public void ShowLabelingTab()
	{
		var vm = new LabelingTabViewModel();

		Content = vm;
	}

	public void ShowArchiveTab()
	{
		var vm = new ArchiveTabViewModel();

		Content = vm;
	}

	public void ShowTrashTab()
	{
		var vm = new TrashTabViewModel();

		Content = vm;
	}
}
