using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnyFileToVideoConverter.Docs {
    internal class BinaryFile {
        public string FilePath => _path;
        public string Hex => _hex;
        public Meta MetaData { get; private set; }
        public int SecLength => _secLength;
        public bool IsEnded => _currentSec > _secLength;
        public int CurrentSec => _currentSec;

        private string _path;
        private int _currentSec = 0; // 0: メタデータ, 1~_secLength: データ
        private string _hex; // 長さは必ず偶数
        private int _secLength;

        const int SINGLE_SEC_LENGTH = 2048;

        public BinaryFile(string path) {
            _path = path;
            _hex = GetHexStringFromFile();
            MetaData = new Meta() {
                filename = Path.GetFileName(path),
                length = _hex.Length
            };

            _secLength = (int) Math.Ceiling((double) (_hex.Length / SINGLE_SEC_LENGTH)) + 1; // よく分からんけど+2が正しいらしい
        }

        private string GetHexStringFromFile() {
            string hex;
            using (var fs = new FileStream(_path, FileMode.Open, FileAccess.Read)) {
                byte[] bs = new byte[fs.Length];
                fs.Read(bs, 0, bs.Length);
                hex = BitConverter.ToString(bs).Replace("-", "");
            }

            return hex;
        }

        public string GetCurrentSecHex() {
            string curSecHex;
            if (_currentSec == 0)
                curSecHex = JsonSerializer.Serialize(MetaData);
            else {
                var curIndex = (_currentSec - 1) * SINGLE_SEC_LENGTH;
                curSecHex = _hex.Substring(
                    curIndex, 
                    (_hex.Length - curIndex + 1) >= SINGLE_SEC_LENGTH ? SINGLE_SEC_LENGTH : _hex.Length - curIndex
                    );
            }
            _currentSec++;

            // Program.Log($"{_currentSec} {_secLength}");
            return curSecHex;
        }
    }
}
