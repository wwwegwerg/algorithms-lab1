using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Plotly.NET;
using Chart = Plotly.NET.CSharp.Chart;

namespace lab1
{
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, string> _tempFiles = new();

        private readonly Dictionary<string, Func<GenericChart>> _charts =
            new()
            {
                ["Точки (Scatter)"] = () =>
                {
                    var x = new[] { 1.0, 2.0, 3.0, 4.0 };
                    var y = new[] { 1.0, 4.0, 9.0, 16.0 };
                    return Chart
                        .Scatter<double, double, string>(x, y, StyleParam.Mode.Markers)
                        .WithTitle("Scatter: y = x²");
                },

                ["Линия (sin)"] = () =>
                {
                    var xs = Enumerable.Range(0, 50).Select(i => i / 10.0).ToArray();
                    var ys = xs.Select(Math.Sin).ToArray();
                    return Chart
                        .Line<double, double, string>(xs, ys)
                        .WithTitle("Line: sin(x)");
                },

                ["Столбцы (Bar)"] = () =>
                {
                    var k = new[] { 1, 2, 3, 4, 5 };
                    var v = new[] { 5.0, 3.0, 6.0, 2.0, 7.0 };
                    return Chart
                        .Column<int, double, string>(k, v)
                        .WithTitle("Bar: sample");
                }
            };

        public MainWindow()
        {
            InitializeComponent();

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
                    path = Path.Combine(Path.GetTempPath(), $"plot_{Sanitize(key)}_{Guid.NewGuid():N}.html");
                    File.WriteAllText(path, html);
                    _tempFiles[key] = path;
                }

                Browser.Url = new Uri(path);
                ;
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
}