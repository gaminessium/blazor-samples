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
                labels = forecasts.Select(f => f.Date.ToShortDateString()),
                datasets = new [] { 
                    new {
                        label = "tempC",
                        data = forecasts.Select(f => f.TemperatureC),
                    },
                    new {
                        label = "tempF",
                        data = forecasts.Select(f => f.TemperatureF),
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