using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;
using NoteTalking.Models;
using ReactiveUI;

namespace NoteTalking.ViewModels
{
	public class NotesTabViewModel : ViewModelBase
	{
		
		private bool _isBusy;
		private NoteViewModel? _selectedNote;
		private readonly ApplicationContext _dbContext = null!;
		private bool _isExpanded;

		private bool IsExpanded
		{
			get => _isExpanded;
			set => this.RaiseAndSetIfChanged(ref _isExpanded, value);
		}
		
		public bool IsCollapsed => !IsExpanded;

		public ReactiveCommand<Unit, bool> ExpandExtPanelCommand { get; }
		public ReactiveCommand<Unit, bool> CollapseExtPanelCommand { get; }
		
		public NotesTabViewModel()
		{
		}
		
		public NotesTabViewModel(ApplicationContext dbContext, MainWindowViewModel mainWindowViewModel)
		{
			_dbContext = dbContext;
			
			ExpandExtPanelCommand = ReactiveCommand.Create(() => IsExpanded = true);
			CollapseExtPanelCommand = ReactiveCommand.Create(() =>
			{
				var current = FocusManager.Instance?.Current as IControl;
				Control parentControl = current.FindLogicalAncestorOfType<StackPanel>();
				
				if (parentControl == null)
				{
					return IsExpanded = false;
				}

				return IsExpanded = true;
			});
			
			this.WhenAnyValue(x => x.IsExpanded)
				.Subscribe(_ => 
				{
					this.RaisePropertyChanged(nameof(IsCollapsed));
				});

			mainWindowViewModel.WhenAnyValue(x => x.SearchText)
				.Throttle(TimeSpan.FromMilliseconds(400))
				.ObserveOn(RxApp.MainThreadScheduler)
				.Subscribe(async searchText => await DoSearch(searchText!));
		}
		
		public bool IsBusy
		{
			get => _isBusy;
			set => this.RaiseAndSetIfChanged(ref _isBusy, value);
		}

		public ObservableCollection<NoteViewModel> SearchResults { get; } = new();
		
		public NoteViewModel? SelectedNote
		{
			get => _selectedNote;
			set => this.RaiseAndSetIfChanged(ref _selectedNote, value);
		}
		
		private async Task DoSearch(string s)
		{
			IsBusy = true;
			SearchResults.Clear();
			
			var notes = await Note.SearchAsync(_dbContext, s);

			foreach (var vm in notes.Select(note => new NoteViewModel(note)))
			{
				SearchResults.Add(vm);
			}
			
			IsBusy = false;
		}
	}
}
