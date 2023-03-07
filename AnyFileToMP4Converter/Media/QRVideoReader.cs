using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyFileToVideoConverter.Media {
    internal class QRVideoReader {
        public string Path => _path;
        public int Length => _length;
        public bool IsEnded => _currentIndex >= _length;
        public int CurrentIndex => _currentIndex;

        private string _path;
        private int _length;
        private int _currentIndex = 0;
        private int _size;
        private VideoCapture _vcap;

        public QRVideoReader(string path) {
            _path = path;
            _vcap = new VideoCapture(path);
            _length = _vcap.FrameCount;
            _size = _vcap.FrameWidth;
        }

        public string ReadCurrentFrame() {
            Program.Log($"processing at {_currentIndex + 1} of {_length}");
            Bitmap bmp = new Bitmap(_size, _size);
            using (var m = new Mat()) {
                if (!_vcap.IsOpened()) {
                    throw new InvalidOperationException("すべてのフレームがすでに読まれています。");
                }

                if (_vcap.Read(m)) {
                    bmp = m.ToBitmap();
                }
            }
            
            _currentIndex++;
            return QR.ReadQRCode(bmp);
        }
    }
}
