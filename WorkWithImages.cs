using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Threading.Tasks;
using System.Threading;
using Point = System.Drawing.Point;

// Явно указываем, какой Point использовать
using Point = System.Drawing.Point;

namespace bot
{
    class WorkWithImages
    {
        public static Mat GetDiffInTwoImages(Bitmap firstState, Bitmap secondState)
        {
            using (Mat img1 = firstState.ToMat())
            using (Mat img2 = secondState.ToMat())
            using (Mat diff = new Mat())
            {
                Cv2.Absdiff(img1, img2, diff);
                Mat mask = new Mat();
                Cv2.CvtColor(diff, mask, ColorConversionCodes.BGR2GRAY);
                Cv2.Threshold(mask, mask, 70, 255, ThresholdTypes.Binary);
                return mask;
            }
        }

        public static Point GetBiggestCountourCoordinates(OpenCvSharp.Point[][] pointsOfCountours)
        {
            if (pointsOfCountours.Length == 0) return new Point(622, 401);

            var biggestContour = pointsOfCountours.OrderByDescending(c => c.Length).First();
            return biggestContour[biggestContour.Length / 2];
        }

        public static bool IsImageMatchWithTemplate(Bitmap monsterRef, Bitmap monsterTemplate)
        {
            using (Mat reference = monsterRef.ToMat())
            using (Mat template = monsterTemplate.ToMat())
            using (Mat result = new Mat())
            {
                Cv2.MatchTemplate(reference.CvtColor(ColorConversionCodes.BGR2GRAY), template.CvtColor(ColorConversionCodes.BGR2GRAY), result, TemplateMatchModes.CCoeffNormed);
                Cv2.Threshold(result, result, 0.7, 1.0, ThresholdTypes.Tozero);
                Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out _);
                return maxVal >= 0.7;
            }
        }

        public static Bitmap BringProcessToFrontAndCaptureGDIWindow(Process process)
        {
            WorkWithProcess.BringProcessWindowToFront(process);
            return CaptureScreen.CaptureWindow(process.MainWindowHandle);
        }
    }
}
