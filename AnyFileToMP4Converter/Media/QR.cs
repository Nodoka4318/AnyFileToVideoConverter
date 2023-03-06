using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.QrCode.Internal;
using ZXing.QrCode;
using ZXing;
using ZXing.Common;

namespace AnyFileToMP4Converter.Media {
    internal class QR {
        public const int QR_SIZE = 300;
        public const int QR_MARGIN = 5;

        public static Bitmap CreateQRCode(string content) {
            var writer = new BarcodeWriter() {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions {
                    Width = QR_SIZE,
                    Height = QR_SIZE,
                    Margin = QR_MARGIN
                }
            };

            return writer.Write(content);
        }
    }
}
