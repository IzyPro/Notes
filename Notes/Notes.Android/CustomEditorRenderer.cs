using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Notes.Droid;
using Notes.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace Notes.Droid
{
    public class CustomEditorRenderer : EditorRenderer, Android.Views.View.IOnLongClickListener
    {
        public CustomEditorRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
            {
                Control.SetOnLongClickListener(this);
            }
        }
        public bool OnLongClick(Android.Views.View v)
        {
            Control.SetTextIsSelectable(true);
            return false;
        }
    }
}