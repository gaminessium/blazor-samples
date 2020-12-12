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
        class TemparatureData
        {
            public string Label { get; set; }
            public IEnumerable<int> Data { get; set; }
        }

        class Temparatures
        {
            public IEnumerable<string> Labels { get; set; }
            public IEnumerable<TemparatureData> Datasets { get; set; }
        }

        protected async void CreateGraph(IReadOnlyCollection<WeatherForecast> forecasts)
        {
            var temparetures = new Temparatures()
            {
                Labels = forecasts.Select(f => f.Date.ToShortDateString()),
                Datasets = new List<TemparatureData>()
                {
                    new TemparatureData()
                    {
                        Label = "tempC",
                        Data = forecasts.Select(f => f.TemperatureC),
                    },
                    new TemparatureData()
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