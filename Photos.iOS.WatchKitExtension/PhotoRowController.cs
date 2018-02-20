#region Using

using Foundation;
using System;
using System.Threading.Tasks;
using Photos.Common;
using Photos.Common.Models;
using UIKit;
using Photos.Common.Helpers;

#endregion

namespace Photos.iOS.WatchKitExtension
{
    public partial class PhotoRowController : NSObject
    {
        #region Constructor

        public PhotoRowController(IntPtr handle) : base(handle)
        {
        }

        #endregion

        #region Methods (Public)

        public async Task SetElement(Photo photo)
        {
            Check.IsNull(photo);

            // Retrieve image data and use it to create UIImage
            var imageData = await PhotosClient.GetImageData(photo);
            var image = UIImage.LoadFromData(NSData.FromArray(imageData));

            // Set image and title
            ImagePhotoPreview.SetImage(image);
            LabelPhotoTitle.SetText(photo.Title);
        }

        #endregion
    }
}