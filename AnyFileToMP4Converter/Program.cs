using AnyFileToVideoConverter.Docs;
using AnyFileToVideoConverter.Media;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace AnyFileToVideoConverter {
    internal class Program {
        static bool isLoggingEnabled = false;

        static void Main(string[] args) {
            string mode;
            string input;
            string output;

            if (args.Length <= 0) {
                Console.WriteLine(@" ________  ________ _________  ___      ___ ________     
|\   __  \|\  _____\\___   ___\\  \    /  /|\   ____\    
\ \  \|\  \ \  \__/\|___ \  \_\ \  \  /  / | \  \___|    
 \ \   __  \ \   __\    \ \  \ \ \  \/  / / \ \  \       
  \ \  \ \  \ \  \_|     \ \  \ \ \    / /   \ \  \____  
   \ \__\ \__\ \__\       \ \__\ \ \__/ /     \ \_______\
    \|__|\|__|\|__|        \|__|  \|__|/       \|_______|
                                                         ");

                Console.Write("# Mode? (encode/decode) >> ");
                mode = Console.ReadLine();
                Console.Write("# Input file path? >> ");
                input = Console.ReadLine();
                Console.Write($"# Output {(mode == "encode" ? "file" : "directory" )} path? >> ");
                output = Console.ReadLine();
            } else if (args.Length < 3) {
                Console.WriteLine("Usage: aftvc.exe <encode/decode> <input file path> <output file/directory path>");
                return;
            } else {
                mode = args[0]; // encode or decode
                input = args[1];
                output = args[2];

                if (args.Length > 3) {
                    Boolean.TryParse(args[3], out isLoggingEnabled);
                }
            }
            
            if (mode == "encode") {
                Encode(input, output, 60);
            } else if (mode == "decode") {
                Decode(input, output);
            } else {
                Console.WriteLine("Usage: aftvc.exe <encode/decode> <input file path> <output file/directory path>");
            }

            if (args.Length <= 0) {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        static unsafe void Encode(string input, string output, int fps) {
            output = output.Replace("\"", "");
            input = input.Replace("\"", "");

            if (File.Exists(output)) {
                Console.Write("# The file already exists. Overwrite? (Y/N) >> ");
                var r = Console.ReadLine();
                if (!(r.ToLower() == "y" || r.ToLower() == "yes")) {
                    return;
                }
            }

            if (Path.GetExtension(output) != "avi" || Path.GetExtension(output) != "mp4") {
                output = Path.ChangeExtension(output, "avi");
            }

            var file = new BinaryFile(input);
            using (var qwriter = new QRVideoWriter(&file)) {
                qwriter.Write(output, fps);
            }

            Console.WriteLine($@"The file has been encoded to {output}.");
        }

        static void Decode(string input, string output) {
            output = output.Replace("\"", "");
            input = input.Replace("\"", "");

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

            Console.WriteLine($@"The file has been decoded to {output}\{file.MetaData.FileName}.");
            if (file.IsCorrupted) {
                Console.WriteLine("The file may be corrupted.");
            }
        }

        public static void Log(string message) {
            if (isLoggingEnabled) {
                Console.WriteLine(message);
            }
        }
    }
}