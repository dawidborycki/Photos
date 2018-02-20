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
    [Register ("InterfaceController")]
    partial class InterfaceController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        WatchKit.WKInterfaceButton ButtonDisplayPhotoList { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        WatchKit.WKInterfaceTable TablePhotos { get; set; }

        [Action ("ButtonDisplayPhotoList_Activated")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ButtonDisplayPhotoList_Activated ();

        void ReleaseDesignerOutlets ()
        {
            if (ButtonDisplayPhotoList != null) {
                ButtonDisplayPhotoList.Dispose ();
                ButtonDisplayPhotoList = null;
            }

            if (TablePhotos != null) {
                TablePhotos.Dispose ();
                TablePhotos = null;
            }
        }
    }
}