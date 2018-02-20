#region Using

using System;
using WatchKit;
using Foundation;
using System.Collections.Generic;
using System.Linq;
using Photos.Common.Models;
using System.Threading.Tasks;
using Photos.Common;

#endregion

namespace Photos.iOS.WatchKitExtension
{
    public partial class InterfaceController : WKInterfaceController
    {
        #region Fields

        private const int rowsPerGroup = 10;
        private IEnumerable<Photo> photos;
        private string[] groupTitles;

        #endregion

        #region Constructor

        protected InterfaceController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        #endregion

        #region View events

        public override void Awake(NSObject context)
        {
            base.Awake(context);

            SetTitle("Hello, watch!");
        }

        public async override void WillActivate()
        {
            if (photos == null)
            {
                // Disable button until the photos are downloaded
                ButtonDisplayPhotoList.SetEnabled(false);

                // Get photos from the web service (first album only)
                photos = await PhotosClient.GetByAlbumId(1);

                // Configure titles for the text input controller
                groupTitles = GetGroupTitles();

                ButtonDisplayPhotoList.SetEnabled(true);
            }
        }

        public override void DidDeactivate()
        {
            // This method is called when the watch view controller is no longer visible to the user.
            Console.WriteLine("{0} did deactivate", this);
        }

        #endregion

        #region Event handlers

        partial void ButtonDisplayPhotoList_Activated()
        {
            PresentTextInputController(groupTitles, WKTextInputMode.Plain, DisplaySelectedPhotos);
        }

        #endregion

        #region Methods (Private)

        private async void DisplaySelectedPhotos(NSArray result)
        {
            if (result != null)
            {
                var menuItem = result.GetItem<NSObject>(0).ToString();

                var rowSelection = RowSelection.Parse(menuItem);

                await LoadTableRows(rowSelection);
            }
        }

        private async Task LoadTableRows(RowSelection rowSelection)
        {
            TablePhotos.SetNumberOfRows(rowSelection.RowCount, "default");

            for (int i = rowSelection.BeginIndex, j = 0; i <= rowSelection.EndIndex; i++, j++)
            {
                var elementRow = (PhotoRowController)TablePhotos.GetRowController(j);

                await elementRow.SetElement(photos.ElementAt(i));
            }
        }

        private string[] GetGroupTitles()
        {
            var groupCount = photos.Count() / rowsPerGroup;

            var titles = new List<string>();

            for (var i = 0; i < groupCount; i++)
            {
                var rowSelection = new RowSelection(
                    i, rowsPerGroup, photos.Count());

                titles.Add(rowSelection.Title);
            }

            return titles.ToArray();
        }

        #endregion
    }
}