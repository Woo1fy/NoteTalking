using System;
using System.Diagnostics;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using NoteTalking.ViewModels;
using ReactiveUI;

namespace NoteTalking.Views;

public partial class NotesTabView : UserControl
{
    public NotesTabView()
    {
        InitializeComponent();
    }

    private void InputElement_OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        NoteTextBox.Focus();
    }
}