using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    private static readonly string Timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
    private static readonly string OutDir = Path.Combine(AppContext.BaseDirectory, Timestamp);

    public MainWindow()
    {
        InitializeComponent();

        Defaults.DefaultDisplayOptions =
            DisplayOptions.init(
                PlotlyJSReference: PlotlyJSReference.Full
            );

        Directory.CreateDirectory(OutDir);
        Preload();

        ChartSelector.ItemsSource = _charts.Keys.ToList();
        ChartSelector.SelectedIndex = 0;
        ChartSelector.SelectionChanged += ChartSelector_SelectionChanged;

        Opened += (_, _) => LoadSelectedChart();
        // Closed += OnClosed;
    }

    private void ChartSelector_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        => LoadSelectedChart();

    private void LoadSelectedChart()
    {
        var key = ChartSelector.SelectedItem as string;
        if (key is null || !_charts.TryGetValue(key, out var build))
            return;

        if (!_tempFiles.TryGetValue(key, out var path) || !File.Exists(path))
        {
            var chart = build().WithConfig(Config.init(Responsive: true));
            var html = GenericChart.toEmbeddedHTML(chart);
            html = EnsureResponsiveUtf8Head(html);
            path = Path.Combine(OutDir, $"plot_{Sanitize(key)}.html");
            File.WriteAllText(path, html, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
            _tempFiles[key] = path;
        }

        Browser.Url = new Uri(path);
        Title = path;
    }

    private static string Sanitize(string name)
    {
        var bad = Path.GetInvalidFileNameChars();
        return new string(name.Select(c => bad.Contains(c) ? '_' : c).ToArray());
    }

    private static string EnsureResponsiveUtf8Head(string html)
    {
        const string head = @"
<meta charset=""utf-8""/>
<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/>
<meta name=""viewport"" content=""width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no""/>
<style>
  html, body {
    height: 100%;
    width: 100%;
    margin: 0; padding: 0;
    overflow: hidden;            /* WKWebView любит скролл — уберём */
  }
  /* plotly контейнеры — на весь доступный размер */
  .js-plotly-plot, .plot-container, .svg-container, .main-svg, .plotly {
    width: 100% !important;
    height: 100% !important;
  }
</style>
<script>
(function(){
  function resizePlots(){
    try {
      var w = document.documentElement.clientWidth;
      var h = document.documentElement.clientHeight;
      var plots = document.getElementsByClassName('js-plotly-plot');
      for (var i = 0; i < plots.length; i++) {
        // подстраховочно обновим размеры контейнера
        plots[i].style.width  = w + 'px';
        plots[i].style.height = h + 'px';
        Plotly.relayout(plots[i], { autosize: true });
        Plotly.Plots.resize(plots[i]);
      }
    } catch(e) { /* noop */ }
  }

  // ResizeObserver даёт самые стабильные ресайзы в WKWebView
  if (typeof ResizeObserver !== 'undefined') {
    var ro = new ResizeObserver(resizePlots);
    ro.observe(document.documentElement);
  }
  window.addEventListener('resize', resizePlots);
  document.addEventListener('DOMContentLoaded', resizePlots);
  window.addEventListener('load', resizePlots);
})();
</script>";

        // если есть <head> — вставим сразу после открывающего тега
        var idxHead = html.IndexOf("<head", StringComparison.OrdinalIgnoreCase);
        if (idxHead >= 0)
        {
            var idxEnd = html.IndexOf('>', idxHead);
            if (idxEnd > idxHead)
            {
                return html.Insert(idxEnd + 1, head);
            }
        }

        // оборачиваем, если <head> отсутствует
        return $@"
<!doctype html>
<html lang=""ru"">
<head>{head}</head>
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
                Console.WriteLine($"File {path} deleted");
            }
            catch
            {
            }
        }

        _tempFiles.Clear();
    }

    private void Preload()
    {
        var keys = _charts.Keys.ToList();
        var sw = new Stopwatch();
        foreach (var key in keys)
        {
            _charts.TryGetValue(key, out var build);

            sw.Restart();
            var chart = build().WithConfig(Config.init(Responsive: true));
            sw.Stop();

            var html = GenericChart.toEmbeddedHTML(chart);
            html = EnsureResponsiveUtf8Head(html);
            var path = Path.Combine(OutDir, $"plot_{Sanitize(key)}.html");
            File.WriteAllText(path, html, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
            _tempFiles[key] = path;

            Console.WriteLine($"{Sanitize(key)} – {sw.Elapsed.TotalSeconds}s");
        }
    }
}