using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using NoteTalking.ViewModels;

namespace NoteTalking.Views;

public partial class MainWindow : Window
{

	public MainWindow()
	{
		InitializeComponent();
	}
	
	private void MainWindow_PointPressed(object sender, PointerPressedEventArgs e)
	{
		GeneralWindow.Focus();
	}
}