using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorSamples.Client.Pages
{
    public partial class Index
    {
        private enum MyEnum { Value1 = 7, Value2 = 5, Value3 = 6 }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;

            var module = await JSRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./js/interop_sample.js"
            );

            await module.InvokeVoidAsync("outputLog", null);
            await module.InvokeVoidAsync("outputLog", true);
            await module.InvokeVoidAsync("outputLog", false);
            // undefined undefined
            // boolean true
            // boolean false

            await module.InvokeVoidAsync("outputLog", 123);
            await module.InvokeVoidAsync("outputLog", "123");
            await module.InvokeVoidAsync("outputLog", "foo");
            // number 123
            // string 123
            // string foo

            var sample_list = new List<string>() { "aaa", "bbb", "ccc" };
            await module.InvokeVoidAsync("outputLog", sample_list);
            await module.InvokeVoidAsync("outputLog", sample_list as IEnumerable<string>);
            // object (3) ["aaa", "bbb", "ccc"]
            // object (3) ["aaa", "bbb", "ccc"]

            await module.InvokeVoidAsync("outputLog", new Dictionary<int, string>() {
                { 1, "value1" },
                { 2, "value2" },
                { 3, "value3" },
            });
            // object {1: "value1", 2: "value2", 3: "value3"}

            await module.InvokeVoidAsync("outputLog", new { item1 = "foo", item2 = "hoo", item3 = 123 });
            // object {item1: "foo", item2: "hoo", item3: 123}

            (int, string) sample_tuple = (909, "sample");
            await module.InvokeVoidAsync("outputLog", sample_tuple);
            await module.InvokeVoidAsync("outputLog", sample_tuple.ToString());
            // object {}
            // string (909, sample)

            await module.InvokeVoidAsync("outputLog", MyEnum.Value1);
            await module.InvokeVoidAsync("outputLog", MyEnum.Value2);
            await module.InvokeVoidAsync("outputLog", MyEnum.Value3);
            // number 7
            // number 5
            // number 6
        }
    }
}
