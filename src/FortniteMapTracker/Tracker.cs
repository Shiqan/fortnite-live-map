using FortniteMapTracker.Core.Models;
using OpenCvSharp;

namespace FortniteMapTracker
{
    public class Tracker
    {
        public Coord Match(string minimap, string world)
        {
            using (var template = new Mat(minimap))
            using (var src = new Mat(world))
            {
                var scaledTemplate = template.Resize(dsize: new Size(template.Width / 2.2, template.Height / 2.2));
                var result = scaledTemplate.MatchTemplate(src, TemplateMatchModes.CCoeffNormed);

                result.MinMaxLoc(out var minVal, out var maxVal, out var minLoc, out var maxLoc);
                Cv2.Threshold(result, result, 0.8, 1.0, ThresholdTypes.Tozero);

                if (maxVal > 0.8)
                {
                    return new Coord(maxLoc.X + (scaledTemplate.Width / 2), maxLoc.Y + (scaledTemplate.Height / 2));
                }
            }

            return null;
        }

        public void DebugMatch(string minimap, string world)
        {
            using (var template = new Mat(minimap))
            using (var src = new Mat(world))
            {
                var scaledTemplate = template.Resize(dsize: new Size(template.Width / 2.2, template.Height / 2.2));
                var result = scaledTemplate.MatchTemplate(src, TemplateMatchModes.CCoeffNormed);

                result.MinMaxLoc(out var minVal, out var maxVal, out var minLoc, out var maxLoc);
                Cv2.Threshold(result, result, 0.8, 1.0, ThresholdTypes.Tozero);

                if (maxVal > 0.8)
                {
                    var point = new Point(maxLoc.X + (scaledTemplate.Width / 2), maxLoc.Y + (scaledTemplate.Height / 2));

                    //Setup the rectangle to draw
                    var r = new Rect(new Point(maxLoc.X, maxLoc.Y), new Size(scaledTemplate.Width, scaledTemplate.Height));

                    //Draw a rectangle of the matching area
                    Cv2.Rectangle(src, r, Scalar.Red, 2);

                    Cv2.ImShow("Matches", src);
                    Cv2.WaitKey();
                }
            }
        }
    }
}
