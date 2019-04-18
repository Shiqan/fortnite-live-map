using FortniteMapTracker;
using FortniteMapTracker.Core;
using FortniteMapTracker.Core.Models;
using FortniteMapTracker.ScreenCapture.Windows;
using FortniteReplayObservers.File;
using FortniteReplayReader.Core.Models;
using Newtonsoft.Json;

namespace FortniteReplayObserver.FileMapTracker
{
    public class TrackerFileObserver : BaseFileObserver<PlayerElimination>
    {
        protected override string CreateMessagePayload(PlayerElimination e)
        {
            if (e.Eliminated == Settings.UserName)
            {
                var payload = GetTrackerPayload();
                payload.Status = TrackerStatus.Eliminated;
                return JsonConvert.SerializeObject(payload);
            }

            if (e.Eliminator == Settings.UserName)
            {
                var payload = GetTrackerPayload();
                payload.Status = TrackerStatus.Eliminator;
                return JsonConvert.SerializeObject(payload);
            }

            return string.Empty;
        }

        private TrackerPayload GetTrackerPayload()
        {
            var world = "./images/world.jpg";
            var tracker = new Tracker();

            using (var minimap = new TempFile("jpeg"))
            using (var screencapturer = new ScreenshotCapture())
            {
                screencapturer.TakeScreenshot(minimap.Path);
                var coord = tracker.Match(minimap.Path, world);

                return new TrackerPayload()
                {
                    Coord = coord
                };
            }
        }

        public class TrackerPayload
        {
            public Coord Coord { get; set; }
            public TrackerStatus Status { get; set; }
        }

        public enum TrackerStatus
        {
            Update,
            Eliminator,
            Eliminated
        }
    }
}

