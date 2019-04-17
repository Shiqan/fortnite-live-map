using FortniteMapTracker.Core;
using FortniteMapTracker.ScreenCapture.Windows;

namespace FortniteMapTracker.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //var minimap = "./images/minimap_org.jpg";
            var world = "./images/world.jpg";
            var tracker = new Tracker();

            using (var minimap = new TempFile("jpeg"))
            using (var screencapturer = new ScreenshotCapture())
            {
                screencapturer.TakeScreenshot(minimap.Path);
                var coord = tracker.Match(minimap.Path, world);
            }

            //var minimaps = Directory.EnumerateFiles("./screenshots/", "*.png");

            //foreach (var minimap in minimaps)
            //{
            //    tracker.Match(minimap, world);
            //}
        }
    }
}
