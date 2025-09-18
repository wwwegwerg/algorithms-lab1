using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Avalonia.Controls;
using Plotly.NET;

namespace lab1.UI;

public partial class MainWindow : Window
{
    private readonly Dictionary<string, string> _tempFiles = new();

    private readonly Dictionary<string, Func<GenericChart>> _charts = ChartRegistry.All;

    public MainWindow()
    {
        InitializeComponent();

        Defaults.DefaultDisplayOptions =
            DisplayOptions.init(
                PlotlyJSReference: PlotlyJSReference.Full
            );

        ChartSelector.ItemsSource = _charts.Keys.ToList();
        ChartSelector.SelectedIndex = 0;
        ChartSelector.SelectionChanged += ChartSelector_SelectionChanged;

        Opened += (_, _) => LoadSelectedChart();
        Closed += OnClosed;
    }

    private void ChartSelector_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        => LoadSelectedChart();

    private void LoadSelectedChart()
    {
        try
        {
            if (Browser is null)
            {
                Title = "Не найден <web:WebView x:Name=\"Browser\"/>";
                return;
            }

            var key = ChartSelector.SelectedItem as string;
            if (key is null || !_charts.TryGetValue(key, out var build))
                return;

            if (!_tempFiles.TryGetValue(key, out var path) || !File.Exists(path))
            {
                var chart = build().WithSize(900, 560); // фиксируем размер
                var html = GenericChart.toEmbeddedHTML(chart);
                html = EnsureUtf8Head(html);
                path = Path.Combine(Path.GetTempPath(), $"plot_{Sanitize(key)}_{Guid.NewGuid():N}.html");
                File.WriteAllText(path, html, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
                _tempFiles[key] = path;
            }

            Browser.Url = new Uri(path);
        }
        catch (Exception ex)
        {
            Title = "Ошибка: " + ex.Message;
        }
    }

    private static string Sanitize(string name)
    {
        var bad = Path.GetInvalidFileNameChars();
        return new string(name.Select(c => bad.Contains(c) ? '_' : c).ToArray());
    }

    private static string EnsureUtf8Head(string html)
    {
        // если <head> есть — вставим мета-теги сразу после него
        var idxHead = html.IndexOf("<head", StringComparison.OrdinalIgnoreCase);
        if (idxHead >= 0)
        {
            var idxEnd = html.IndexOf('>', idxHead);
            if (idxEnd > idxHead)
            {
                var inset = "\n<meta charset=\"utf-8\"/>" +
                            "\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\n";
                // проверим, нет ли уже charset
                if (html.IndexOf("charset", idxHead, StringComparison.OrdinalIgnoreCase) < 0)
                    html = html.Insert(idxEnd + 1, inset);
                return html;
            }
        }

        // если <head> отсутствует — обернём в минимальный HTML
        return
            $@"<!doctype html>
<html lang=""ru"">
<head>
<meta charset=""utf-8""/>
<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/>
<meta name=""viewport"" content=""width=device-width, initial-scale=1""/>
<title>Plot</title>
</head>
<body>
{html}
</body>
</html>";
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        foreach (var path in _tempFiles.Values.Distinct())
        {
            try
            {
                if (File.Exists(path)) File.Delete(path);
            }
            catch
            {
            }
        }

        _tempFiles.Clear();
    }
}