using System;
using System.Activities;
using System.ComponentModel;
using System.Drawing;
using UiPath.Core;
using UiPath.Activities;
using System.IO;

namespace Impower.UiAutomation.Bitmap
{
    [DisplayName("UiElement to Bitmap")]
    public class UiElementToBitmap : CodeActivity
    {
        [Category("Input")]
        [Description("UiElement to convert to bitmap.")]
        [DisplayName("Input UiElement")]
        [RequiredArgument]
        public InArgument<UiElement> InputElement { get; set; }

        [Category("Output")]
        [Description("Resulting bitmap.")]
        [DisplayName("Output Bitmap")]
        public OutArgument<System.Drawing.Bitmap> OutputBitmap { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            UiElement inputEl = InputElement.Get(context);
            UiPath.Core.Image screenshot = inputEl.Screenshot();
            byte[] screenshotByteArr = screenshot.ByteArray;
            System.Drawing.Bitmap bmp;
            using (var ms = new MemoryStream(screenshotByteArr))
            {
                bmp = new System.Drawing.Bitmap(ms);
            }
            OutputBitmap.Set(context, bmp);
        }
    }
}
