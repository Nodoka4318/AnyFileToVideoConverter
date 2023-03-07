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
        public const int QR_SIZE = 400; // たぶんこれが最適
        public const int QR_MARGIN = 40; // TODO: パラメータ化

        public static Bitmap CreateQRCode(string content) {
            FLAG_UNDO:

            var writer = new BarcodeWriter() {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions {
                    ErrorCorrection = ErrorCorrectionLevel.L,
                    CharacterSet = "UTF-8",
                    Width = QR_SIZE,
                    Height = QR_SIZE,
                    Margin = QR_MARGIN
                }
            };

            var res = writer.Write(content);

            if (ReadQRCode(res) != content) {
                goto FLAG_UNDO; // ゆるして
            }

            return res;
        }

        public static string ReadQRCode(Bitmap qrcode) {
            var reader = new BarcodeReader() {
                Options = new DecodingOptions() {
                    PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.QR_CODE }
                }
            };
            // qrcode.Save(@"C:\Users\Nodoka\Desktop\curqr.png", ImageFormat.Png);
            var res = reader.Decode(qrcode);
            if (res == null) {
                qrcode.Save(@"C:\Users\Nodoka\Desktop\curqr.png", ImageFormat.Png);
                throw new Exception("QRコードの読み取りに失敗しました。");
            } else {
                return res.Text;
            }
        }
    }
}
