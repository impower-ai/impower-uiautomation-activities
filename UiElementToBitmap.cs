using System;
using System.Activities;
using System.ComponentModel;
using System.Drawing;
using UiPath.Core;
using UiPath.Activities;
using System.IO;

namespace Impower.UiAutomation
{
    public class UiElementToBitmap : CodeActivity
    {
        [RequiredArgument]
        public InArgument<UiElement> InputElement { get; set; }
        public OutArgument<Bitmap> OutputBitmap { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            UiElement inputEl = InputElement.Get(context);
            UiPath.Core.Image screenshot = inputEl.Screenshot();
            byte[] screenshotByteArr = screenshot.ByteArray;
            Bitmap bmp;
            using (var ms = new MemoryStream(screenshotByteArr))
            {
                bmp = new Bitmap(ms);
            }
            OutputBitmap.Set(context, bmp);
        }
    }
}
