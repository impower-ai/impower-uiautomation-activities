using System;
using System.Activities;
using System.ComponentModel;
using System.Drawing;
using UiPath.Core;
using UiPath.Activities;
using System.IO;
using System.Text;

namespace Impower.UiAutomation
{
    public class BitmapInfo : CodeActivity
    {
        [RequiredArgument]
        public InArgument<Bitmap> InputBitmap { get; set; }
        public OutArgument<Color> AverageColor { get; set; }
        public OutArgument<float> AverageBrightness { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            BitmapInfoObj bitmapInfo = GetInfo(InputBitmap.Get(context));
            AverageColor.Set(context, bitmapInfo.AverageColor);
            AverageBrightness.Set(context, bitmapInfo.AverageBrightness);
        }

        private class BitmapInfoObj
        {
            public Color AverageColor { get; set; }
            public float AverageBrightness { get; set; }
        }

        private BitmapInfoObj GetInfo(Bitmap b)
        {
            float brightnessTotal = 0;
            int redTotal = 0;
            int greenTotal = 0;
            int blueTotal = 0;
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    Color pixelColor = b.GetPixel(x, y);
                    redTotal += pixelColor.R;
                    greenTotal += pixelColor.G;
                    blueTotal += pixelColor.B;
                    brightnessTotal += pixelColor.GetBrightness();
                }
            }
            int totalPixels = b.Width * b.Height;
            redTotal /= totalPixels;
            greenTotal /= totalPixels;
            blueTotal /= totalPixels;
            var bitmapInfoObj = new BitmapInfoObj
            {
                AverageColor = Color.FromArgb(redTotal, greenTotal, blueTotal),
                AverageBrightness = brightnessTotal / totalPixels
            };
            return bitmapInfoObj;
        }
    }
}
