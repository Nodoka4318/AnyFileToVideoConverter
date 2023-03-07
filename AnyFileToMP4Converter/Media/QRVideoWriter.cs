using AnyFileToMP4Converter.Docs;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyFileToMP4Converter.Media {
    internal unsafe class QRVideoWriter : IDisposable {
        private BinaryFile* _file;
        private bool _disposed = false;

        public QRVideoWriter(BinaryFile* file) {
            GC.SuppressFinalize(*file);
            _file = file;
        }

        public void Write(string path, int fps) {
            using (var writer = new VideoWriter(path, FourCC.H264, fps, new Size(QR.QR_SIZE, QR.QR_SIZE))) {
                while (!_file->IsEnded) {
                    using (var m = QR.CreateQRCode(_file->GetCurrentSecHex()).ToMat()) {
                        Console.WriteLine($"Writing {_file->CurrentSec} of {_file->SecLength + 1}");
                        writer.Write(m);
                    }
                }
            }
        }

        public void Dispose() {
            Dispose(true);
            // GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing) {
                    // マネージドリソースの開放
                }

                GC.ReRegisterForFinalize(*_file);
                _disposed = true;
            }
        }

        ~QRVideoWriter() {
            Dispose(false);
        }
    }
}
