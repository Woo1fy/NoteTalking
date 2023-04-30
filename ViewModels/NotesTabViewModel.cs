using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using NoteTalking.Models;
using ReactiveUI;

namespace NoteTalking.ViewModels
{
	public class NotesTabViewModel : ViewModelBase
	{
		
		private bool _isBusy;
		private NoteViewModel? _selectedNote;
		private readonly ApplicationContext _dbContext = null!;
		
		public NotesTabViewModel()
		{
		}
		
		public NotesTabViewModel(ApplicationContext dbContext, MainWindowViewModel mainWindowViewModel)
		{
			_dbContext = dbContext;
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
