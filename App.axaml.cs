using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NoteTalking.ViewModels;
using NoteTalking.Views;


namespace NoteTalking;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var context = new ApplicationContext();
            
            context.Database.EnsureCreated();
                
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(context),
            };
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}