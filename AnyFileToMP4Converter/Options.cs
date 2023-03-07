using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyFileToVideoConverter {
    internal class Options {
        [CommandLine.Option('e', "encode", Default = "", HelpText = "the file you want to encode")]
        public string EncodeFile { get; set; }
        
        [CommandLine.Option('d', "decode", Default = "", HelpText = "the video file you want to decode")]
        public string DecodeFile { get; set; }

        [CommandLine.Option('o', "output", Default = "", HelpText = "the directory/file (where) you want to output")]
        public string Output { get; set; }

        [CommandLine.Option('l', "logging", Default = false, HelpText = "whether to display the log or not")]
        public bool Logging { get; set; }

        [CommandLine.Option('f', "fps", Default = 60, HelpText = "custom fps")]
        public int Fps { get; set; }
    }
}
