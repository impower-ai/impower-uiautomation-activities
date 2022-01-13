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
    [DisplayName("Get Bitmap Info")]
    public class GetBitmapInfo : CodeActivity
    {
        [Category("Input")]
        [Description("Input Bitmap to get info from.")]
        [DisplayName("Input Bitmap")]
        [RequiredArgument]
        public InArgument<Bitmap> InputBitmap { get; set; }

        [Category("Output")]
        [Description("Resulting Bitmap Info Object")]
        [DisplayName("Output Bitmap Info Object")]
        public OutArgument<ImpowerBitmapInfoObj> OutputBitmapInfo { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            ImpowerBitmapInfoObj bitmapInfo = GetInfo(InputBitmap.Get(context));
            OutputBitmapInfo.Set(context, bitmapInfo);
        }

        public class ImpowerBitmapInfoObj
        {
            public Color AverageColor { get; set; }
            public float AverageBrightness { get; set; }
        }

        private ImpowerBitmapInfoObj GetInfo(Bitmap b)
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
            var bitmapInfoObj = new ImpowerBitmapInfoObj
            {
                AverageColor = Color.FromArgb(redTotal, greenTotal, blueTotal),
                AverageBrightness = brightnessTotal / totalPixels
            };
            return bitmapInfoObj;
        }
    }
}
