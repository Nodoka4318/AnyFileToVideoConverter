# AFTVC.exe
This software allows any data to be archived as a video by encoding the binary into a QR code.

## Screenshot
### App
<img width="867" alt="image" src="https://user-images.githubusercontent.com/78198198/223365035-25350706-ecc2-4922-8601-ac0351b22424.png">

### Output Video File
<img width="384" alt="image" src="https://user-images.githubusercontent.com/78198198/223365760-6c358e10-79a6-444c-a049-4ba12d02a060.png">

## Usage
You can execute this program with command, or just simply double click the exe file ;)

### with cmd
- Encoding
```ps1
aftvc.exe encode path/to/inputfile path/to/outputfile
```
- Decoding
```ps1
aftvc.exe decode path/to/inputfile path/to/outputfile
```
If you set "true" as the fourth argument, you can see my stupid logs.

## TODO
- [ ] Customization of the amount of data in the QR code
- [ ] Video compression
## Credits
My thanks to the developers who are providing us with a wonderful packages.
- [ZXing.NET](https://github.com/micjahn/ZXing.Net)
- [OpenH264](https://github.com/cisco/openh264)
- [OpenCvSharp](https://github.com/shimat/opencvsharp)
