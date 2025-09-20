using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.WebView.Desktop;

namespace lab1.UI;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI()
            .UseDesktopWebView();

    // public static void Main(string[] args)
    // {
    //     var n = 10;
    //     var zData = new double[n][];
    //     for (var i = 0; i < n; i++)
    //     {
    //         zData[i] = new double[n];
    //         for (var j = 0; j < n; j++)
    //         {
    //             zData[i][j] = i * n + j;
    //         }
    //     }
    //
    //     foreach (var z in zData)
    //     {
    //         foreach (var x in z)
    //         {
    //             Console.WriteLine(x);
    //         }
    //     }
    // }
}