using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using NoteTalking.Models;

namespace NoteTalking.ViewModels
{
	public class NotesTabViewModel : ViewModelBase
	{
		// private readonly DataContext _context = new DataContext();
		//
		public ObservableCollection<NoteItem> Notes { get; set; }
		public NotesTabViewModel(DataContext context)
		{
			context.Notes.Load();
			Notes = context.Notes.Local.ToObservableCollection();
		}
		
		
	}
}
