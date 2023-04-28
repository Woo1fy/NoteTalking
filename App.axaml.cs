using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NoteTalking.ViewModels;
using NoteTalking.Views;

namespace NoteTalking;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();

        if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
        var db = new DataContext();

        desktop.MainWindow = new MainWindow
        {
            DataContext = new MainWindowViewModel(db),
        };
    }
}