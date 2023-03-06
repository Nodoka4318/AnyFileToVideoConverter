using AnyFileToMP4Converter.Docs;
using AnyFileToMP4Converter.Media;
using System.Drawing.Imaging;

namespace AnyFileToMP4Converter {
    internal class Program {
        static unsafe void Main(string[] args) {

            // var filepath = @"C:\Users\Nodoka\Desktop\test.bin";
            var filepath = @"C:\Users\Nodoka\Downloads\DotGothic16.zip";
            var savepath = @"C:\Users\Nodoka\Desktop\testqr.avi";
            var bin = new BinaryFile(filepath);
            var writer = new QRVideoWriter(&bin);
            var task = writer.WriteAsync(savepath, 30);
            Console.WriteLine("Completed!");
            
            /*
            var qr = QR.CreateQRCode("あかさたな");
            qr.Save(@"C:\Users\Nodoka\Desktop\testqr.png", ImageFormat.Png);
            */
        }
    }
}