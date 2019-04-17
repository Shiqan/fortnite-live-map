using FortniteMapTracker.Core.Contracts;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace FortniteMapTracker.ScreenCapture.Windows
{
    public class ScreenshotCapture : IScreenshotCapture, IDisposable
    {
        private Bitmap _bitmap;
        private Graphics _graphics;

        public void TakeScreenshot(string path)
        {
            _bitmap = new Bitmap(375, 375, PixelFormat.Format32bppArgb);
            _graphics = Graphics.FromImage(_bitmap);

            _graphics.CopyFromScreen(2560 - 375 - 18, 18, 0, 0, new Size(375, 375), CopyPixelOperation.SourceCopy);
            _bitmap.Save(path, ImageFormat.Jpeg);
        }

        public void Dispose()
        {
            _bitmap?.Dispose();
            _graphics?.Dispose();
        }
    }
}
