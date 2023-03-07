using AnyFileToVideoConverter.Docs;
using AnyFileToVideoConverter.Media;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace AnyFileToVideoConverter {
    internal class Program {
        static bool isLoggingEnabled = false;

        static void Main(string[] args) {
            var result = CommandLine.Parser.Default.ParseArguments<Options>(args);

            var encodeFile = result.Value.EncodeFile;
            var decodeFile = result.Value.DecodeFile;
            var output = result.Value.Output;
            var logging = result.Value.Logging;
            var fps = result.Value.Fps;

            if (logging) {
                isLoggingEnabled = true;
            }

            if (encodeFile != "" && decodeFile == "" && output != "") {
                Encode(encodeFile, output, fps);
            } else if (encodeFile == "" && decodeFile != "" && output != "") {
                Decode(decodeFile, output);
            } else {
                Console.WriteLine("Invalid arguments has been given. Please see '--help'.");
            }
        }

        static unsafe void Encode(string input, string output, int fps) {
            if (File.Exists(output)) {
                Console.Write("# The file already exists. Overwrite? (Y/N) >> ");
                var r = Console.ReadLine();
                if (!(r.ToLower() == "y" || r.ToLower() == "yes")) {
                    return;
                }
            }


            var file = new BinaryFile(input);
            using (var qwriter = new QRVideoWriter(&file)) {
                qwriter.Write(output, fps);
            }
        }

        static void Decode(string input, string output) {
            if (!Directory.Exists(output)) {
                Console.Write("# The directory does not exist. Create it? (Y/N) >> ");
                var r = Console.ReadLine();
                if (!(r.ToLower() == "y" || r.ToLower() == "yes")) {
                    return;
                } else {
                    Directory.CreateDirectory(output);
                }
            }

            var qreader = new QRVideoReader(input);
            var file = new QRFile(qreader);
            file.ReadData();
            file.SaveFile(output);
        }

        public static void Log(string message) {
            if (isLoggingEnabled) {
                Console.WriteLine(message);
            }
        }
    }
}