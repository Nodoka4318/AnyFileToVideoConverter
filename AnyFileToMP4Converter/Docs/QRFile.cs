using AnyFileToMP4Converter.Media;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnyFileToMP4Converter.Docs {
    internal class QRFile {
        public Meta MetaData { get; private set; }

        private QRVideoReader _reader;
        private byte[] _bytes;

        public QRFile(QRVideoReader reader) {
            _reader = reader;
        }

        public void ReadData() {
            var bs = new List<byte>();

            while (!_reader.IsEnded) {
                if (_reader.CurrentIndex == 0) {
                    var metaStr = _reader.ReadCurrentFrame();
                    MetaData = JsonSerializer.Deserialize<Meta>(metaStr);
                    continue;
                }

                var currentHex = _reader.ReadCurrentFrame();
                for (int i = 0; i < currentHex.Length / 2; i++) {
                    bs.Add(Convert.ToByte(currentHex.Substring(i * 2, 2), 16));
                }
            }

            _bytes = bs.ToArray(); // 複製
            bs.Clear(); // 解放
        }

        public void SaveFile(string directory) {
            if (_bytes.Length <= 0) {
                throw new InvalidOperationException("まだデータが読まれていません。");
            }
            File.WriteAllBytes(directory + $@"\{MetaData.filename}", _bytes);
        }
    }
}
