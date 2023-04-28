using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace NoteTalking.Views;

public partial class MainWindow : Window
{
	private List<Note> NotesCollection { get; set; } = new List<Note>();

    public MainWindow()
    {
        InitializeComponent();

  //      AddNoteBox.GotFocus += AddNoteBox_GotFocus;
  //      AddNoteBox.LostFocus += AddNoteBox_LostFocus;
		//AddNoteBox.KeyUp += AddNoteBox_KeyUp;
		SearchNoteBox.PropertyChanged += SearchNoteBox_PropertyChanged;
		SearchNoteBox.AddHandler(TextInputEvent, SearchNoteBox_TextInput, RoutingStrategies.Tunnel);
	}

	private void SearchNoteBox_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
	{
		//if (e.Property.Name != nameof(SearchNoteBox.Text)
		//	|| sender is not TextBox tb
		//	|| !NotesCollection.Any()) { return; }

		//var sortedCollection = NotesCollection.Where(x => x!.NoteBox.Text.Contains(e.NewValue!.ToString() ?? string.Empty, StringComparison.OrdinalIgnoreCase));
		//if (sortedCollection != null && sortedCollection.Any())
		//{
		//	NotesPanel.Children.Clear();
		//	NotesPanel.Children.AddRange(sortedCollection);
		//	return;
		//}

		//NotesPanel.Children.Clear();
		//NotesPanel.Children.AddRange(NotesCollection);
	}

	private void SearchNoteBox_TextInput(object? sender, TextInputEventArgs e)
	{
		
	}

	private void AddNoteBox_KeyUp(object? sender, KeyEventArgs e)
	{
		if (sender is not TextBox tb) { return; }

		if (e.Key == Key.Enter)
		{
			AddNote(tb);
		}
	}

	private void AddNoteBox_GotFocus(object? sender, EventArgs e)
    {
		if (sender is not TextBox) { return; }


	}

	private void AddNoteBox_LostFocus(object? sender, EventArgs e)
	{
		if (sender is not TextBox tb) { return; }

		AddNote(tb);
	}

	#region Logic
		
	private void AddNote(TextBox tb)
	{
		if (!string.IsNullOrEmpty(tb.Text))
		{
			Note note = new();
			note.NoteBox.Text = tb.Text;

			NotesCollection.Insert(0, note);
			//NotesPanel.Children.Insert(0, NotesCollection.FirstOrDefault());


			//AddNoteBox.Text = string.Empty;
		}
	}

	#endregion
}