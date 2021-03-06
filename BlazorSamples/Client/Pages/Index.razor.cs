﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorSamples.Client.Pages
{
    public partial class Index
    {
        private enum MyEnum { Value1 = 7, Value2 = 5, Value3 = 6 }

        class SampleClass
        {
            private string _X = "private field";
            public string X = "public field";
            private string _Y { get; set; } = "private property";
            public string Y { get; set; } = "public property";
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;

            var module = await JSRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./js/interop-sample.js"
            );

            await module.InvokeVoidAsync("outputLog", null);
            await module.InvokeVoidAsync("outputLog", true);
            await module.InvokeVoidAsync("outputLog", false);
            // null は undefined へ、bool は boolean へそれぞれ変換される。
            //   undefined undefined
            //   boolean true
            //   boolean false

            await module.InvokeVoidAsync("outputLog", 123);
            await module.InvokeVoidAsync("outputLog", "123");
            await module.InvokeVoidAsync("outputLog", "foo");
            // 数値を渡した場合は number へ、文字列を渡した場合は string へ変換される。
            //   number 123
            //   string 123
            //   string foo

            // enum MyEnum { Value1 = 7, Value2 = 5, Value3 = 6 }
            await module.InvokeVoidAsync("outputLog", MyEnum.Value1);
            await module.InvokeVoidAsync("outputLog", MyEnum.Value2);
            await module.InvokeVoidAsync("outputLog", MyEnum.Value3);
            // 列挙型のメンバーは、対応する整数値へ変換される。
            //   number 7
            //   number 5
            //   number 6

            var sampleList = new List<string>() { "aaa", "bbb", "ccc" };
            await module.InvokeVoidAsync("outputLog", sampleList);
            await module.InvokeVoidAsync("outputLog", sampleList as IEnumerable<string>);
            // IEnumerable インターフェイスを備えた型であれば配列へ変換される。
            //   object (3) ["aaa", "bbb", "ccc"]
            //   object (3) ["aaa", "bbb", "ccc"]

            // class SampleClass
            // {
            //     private string _X = "private field";
            //     public string X = "public field";
            //     private string _Y { get; set; } = "private property";
            //     public string Y { get; set; } = "public property";
            // }
            var sampleInstance = new SampleClass();
            await module.InvokeVoidAsync("outputLog", sampleInstance);
            // クラスインスタンスは public プロパティのみを含んだ object へ変換される。
            //   object {y: "public property"}

            await module.InvokeVoidAsync("outputLog", new Dictionary<int, string>() {
                { 1, "value1" },
                { 2, "value2" },
                { 3, "value3" },
            });
            // Dictionary は object へ変換される。
            //   object {1: "value1", 2: "value2", 3: "value3"}

            await module.InvokeVoidAsync("outputLog", new
            {
                Item1 = "foo",
                Item2 = "hoo",
                Item3 = 123
            });
            // 匿名型も object へ変換される。
            //   object {item1: "foo", item2: "hoo", item3: 123}

            var sampleTuple = (909, 703);
            await module.InvokeVoidAsync("outputLog", sampleTuple);
            await module.InvokeVoidAsync("outputLog", sampleTuple.ToString());
            // Tuple は変換を行えない。
            //   object {}
            //   string (909, 703)

            var sampleValueTuple = (x: 123, y: "sample");
            await module.InvokeVoidAsync("outputLog", sampleValueTuple);
            await module.InvokeVoidAsync("outputLog", sampleValueTuple.ToString());
            // ValueTuple も同様。
            //   object {}
            //   string (123, sample)
        }
    }
}
