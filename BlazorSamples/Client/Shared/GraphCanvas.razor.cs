using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSamples.Client.Shared
{
    public partial class GraphCanvas
    {
        public class LineData
        {
            public string Label { get; set; }
            public IEnumerable<int> Data { get; set; }
            public double Tension { get; set; } = 0.5;
            public string BorderColor { get; set; } = "red";
        }

        public class LineGraphData
        {
            public IEnumerable<string> Labels { get; set; }
            public IEnumerable<LineData> Datasets { get; set; }
        }

        public class ResponsiveOption
        {
            public bool Responsive { get; set; } = true;
        }

        public class LineGraph
        {
            public string Type { get; } = "line";
            public LineGraphData Data { get; set; }
            public ResponsiveOption Options { get; set; } = new ResponsiveOption();
        }
    }
}
