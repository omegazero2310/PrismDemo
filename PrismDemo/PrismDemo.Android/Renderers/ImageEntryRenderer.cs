using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using AndroidX.Core.Content;
using PrismDemo.Droid.Renderers;
using PrismDemo.Exts.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRenderer))]
namespace PrismDemo.Droid.Renderers
{
    public class ImageEntryRenderer : EntryRenderer
    {
        ImageEntry element;

        public ImageEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            element = (ImageEntry)this.Element;


            var editText = this.Control;
            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Image), null, null, null);
                        break;
                    case ImageAlignment.Right:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.Image), null);
                        break;
                }
            }
            editText.CompoundDrawablePadding = 25;
            Control.Background.SetColorFilter(new PorterDuffColorFilter(element.LineColor.ToAndroid(), PorterDuff.Mode.SrcAtop));
        }

        private BitmapDrawable GetDrawable(string imageEntryImage)
        {
            string imageName = imageEntryImage.Replace(".png","").Replace(".jpg","");
            int resID = Resources.GetIdentifier(imageName, "drawable", this.Context.PackageName);
            var drawable = this.Context.GetDrawable(resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * 2, element.ImageHeight * 2, true));
        }

    }
}