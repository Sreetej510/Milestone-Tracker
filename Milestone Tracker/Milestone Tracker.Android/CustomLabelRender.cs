using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Milestone_Tracker.Models.CustomLabel), typeof(Milestone_Tracker.Droid.CustomLabelRender))]

namespace Milestone_Tracker.Droid
{
    public class CustomLabelRender : LabelRenderer
    {
        public CustomLabelRender(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.JustificationMode = Android.Text.JustificationMode.InterWord;
            }
        }
    }
}