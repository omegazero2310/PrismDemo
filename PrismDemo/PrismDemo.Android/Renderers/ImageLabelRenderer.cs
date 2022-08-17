using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using PrismDemo.Droid.Renderers;
using PrismDemo.Exts.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImageLabel), typeof(ImageLabelRenderer))]
namespace PrismDemo.Droid.Renderers
{
    public class ImageLabelRenderer : LabelRenderer
    {
        ImageLabel element;

        public ImageLabelRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            element = (ImageLabel)this.Element;


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
        }

        private BitmapDrawable GetDrawable(string imageEntryImage)
        {
            string imageName = imageEntryImage.Replace(".png", "").Replace(".jpg", "");
            int resID = Resources.GetIdentifier(imageName, "drawable", this.Context.PackageName);
            var drawable = this.Context.GetDrawable(resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * 2, element.ImageHeight * 2, true));
        }

    }
}