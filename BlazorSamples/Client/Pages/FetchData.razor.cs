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
        protected async void CreateGraph(IReadOnlyCollection<WeatherForecast> forecasts)
        {
            var temparetures = new {
                Labels = forecasts.Select(f => f.Date.ToShortDateString()),
                Datasets = new [] { 
                    new {
                        Label = "tempC",
                        Data = forecasts.Select(f => f.TemperatureC),
                    },
                    new {
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