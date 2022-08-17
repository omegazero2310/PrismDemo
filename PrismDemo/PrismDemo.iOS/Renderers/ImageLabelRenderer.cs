//PrismDemo.iOS.Renderers
using CoreAnimation;
using CoreGraphics;
using Foundation;
using PrismDemo.Exts.CustomControls;
using PrismDemo.iOS.Renderers;
using System;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImageLabel), typeof(ImageLabelRenderer))]
namespace PrismDemo.iOS.Renderers
{
    public class ImageLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            var element = (ImageLabel)this.Element;
            var labelField = this.Control;
            var imageLabel = new NSTextAttachment();
            imageLabel.Image = GetImageView(element.Image, element.ImageHeight, element.ImageWidth).ConvertToImage();
            var imageOffsetY = (nfloat)(- 5.0);
            imageLabel.Bounds = new CGRect(x: 0, y: imageOffsetY, width: (nfloat)imageLabel.Image?.Size.Width, height: (nfloat)imageLabel.Image?.Size.Height);
            var attachmentString = NSAttributedString.FromAttachment(imageLabel);
            var completeText = new NSMutableAttributedString("");       
            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        completeText.Append(attachmentString);
                        var textAfterIcon = new NSAttributedString(labelField.Text);
                        completeText.Append(textAfterIcon);
                        labelField.TextAlignment = UITextAlignment.Center;
                        labelField.AttributedText = completeText;
                        break;
                    case ImageAlignment.Right:
                        var textBeforeIcon = new NSAttributedString(labelField.Text);
                        completeText.Append(textBeforeIcon);
                        completeText.Append(attachmentString);
                        labelField.TextAlignment = UITextAlignment.Center;
                        labelField.AttributedText = completeText;
                        break;
                }
            }
        }

        private UIView GetImageView(string imagePath, int height, int width)
        {
            var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))
            {
                Frame = new RectangleF(0, 0, width, height)
            };
            UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, width + 10, height));
            objLeftView.AddSubview(uiImageView);

            return objLeftView;
        }
    }
}