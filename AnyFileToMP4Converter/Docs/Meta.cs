using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyFileToVideoConverter.Docs {
    internal class Meta {
        public string FileName { get; set; }
        public int Length { get; set; }

        // NativeAOTだとJsonSerializer動かないからしゃーなし
        public static Meta FromText(string text) {
            var vals = text.Split("<>");
            return new Meta() {
                FileName = vals[0],
                Length = int.Parse(vals[1])
            };
        }

        public string BuildText() {
            return $"{FileName}<>{Length}";
        }
    }
}
