using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorSamples.Shared;
using Microsoft.JSInterop;

namespace BlazorSamples.Client.Pages
{
    public partial class FetchData
    {
        class LineData
        {
            public string Label { get; set; }
            public IEnumerable<int> Data { get; set; }
        }

        class LineGraphData
        {
            public IEnumerable<string> Labels { get; set; }
            public IEnumerable<LineData> Datasets { get; set; }
        }

        protected async void CreateGraph(IReadOnlyCollection<WeatherForecast> forecasts)
        {
            var temparetures = new LineGraphData()
            {
                Labels = forecasts.Select(f => f.Date.ToShortDateString()),
                Datasets = new List<LineData>()
                {
                    new LineData()
                    {
                        Label = "tempC",
                        Data = forecasts.Select(f => f.TemperatureC),
                    },
                    new LineData()
                    {
                        Label = "tempF",
                        Data = forecasts.Select(f => f.TemperatureF),
                    }
                }
            };

            var module = await JSRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./js/chart-companion.js"
            );

            await module.InvokeVoidAsync(
                "createGraph",
                graphCanvas,
                temparetures
            );
        }
    }
}