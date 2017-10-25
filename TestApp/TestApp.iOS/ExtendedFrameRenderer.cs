using CoreGraphics;
using StrapUp.Forms.iOS;
using StrapUp.Forms.Controls;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedFrame), typeof(ExtendedFrameRenderer))]

namespace StrapUp.Forms.iOS
{
    public class ExtendedFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {

            base.OnElementChanged(e);
            var elem = (ExtendedFrame)this.Element;
            if (elem != null)
            {

                // Border
                this.Layer.CornerRadius = (float)elem.Radius;
                this.Layer.Bounds.Inset((int)elem.BorderWidth, (int)elem.BorderWidth);
                Layer.BorderColor = elem.BorderColor.ToCGColor();
                Layer.BorderWidth = (float)elem.BorderWidth;
                Layer.ShadowColor = elem.BorderColor.ToCGColor();

                // Layer.ShadowRadius = (float)elem.Radius;

            }
        }

    }
}