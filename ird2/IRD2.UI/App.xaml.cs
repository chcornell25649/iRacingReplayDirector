using System.Configuration;
using System.Data;
using System.Windows;

namespace IRD2.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
        {
            var exception = args.ExceptionObject as Exception;
            if (exception != null)
            {
                MessageBox.Show($"Startup Error: {exception.Message}\n\n{exception.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        };
        
        DispatcherUnhandledException += (sender, args) =>
        {
            MessageBox.Show($"UI Error: {args.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            args.Handled = true;
        };
    }
}

