// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Photos.iOS.WatchKitExtension
{
    [Register ("PhotoRowController")]
    partial class PhotoRowController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        WatchKit.WKInterfaceImage ImagePhotoPreview { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        WatchKit.WKInterfaceLabel LabelPhotoTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ImagePhotoPreview != null) {
                ImagePhotoPreview.Dispose ();
                ImagePhotoPreview = null;
            }

            if (LabelPhotoTitle != null) {
                LabelPhotoTitle.Dispose ();
                LabelPhotoTitle = null;
            }
        }
    }
}