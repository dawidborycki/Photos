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
        private WKAlertAction[] alertActions;

        #endregion

        #region Constructor

        protected InterfaceController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        #endregion

        #region View events

        public async override void Awake(NSObject context)
        {
            base.Awake(context);

            SetTitle("Hello, watch!");

            // Disable button until the photos are downloaded
            ButtonDisplayPhotoList.SetEnabled(false);

            // Get photos from the web service (first album only)
            photos = await PhotosClient.GetByAlbumId(1);

            // Create actions for the alert
            CreateAlertActions();

            ButtonDisplayPhotoList.SetEnabled(true);
        }

        public override void WillActivate()
        {            
        }

        public override void DidDeactivate()
        {            
        }

        #endregion

        #region Event handlers

        partial void ButtonDisplayPhotoList_Activated()
        {            
            PresentAlertController(string.Empty, string.Empty, 
                                   WKAlertControllerStyle.ActionSheet, alertActions);
        }

        #endregion

        #region Methods (Private)

        private void CreateAlertActions()
        {
            var actionsCount = photos.Count() / rowsPerGroup;

            alertActions = new WKAlertAction[actionsCount];

            for (var i = 0; i < actionsCount; i++)
            {
                var rowSelection = new RowSelection(
                    i, rowsPerGroup, photos.Count());

                var alertAction = WKAlertAction.Create(rowSelection.Title,
                                                       WKAlertActionStyle.Default,
                                                       async () => { await DisplaySelectedPhotos(rowSelection); });

                alertActions[i] = alertAction;
            }
        }

        private async Task DisplaySelectedPhotos(RowSelection rowSelection)
        {
            TablePhotos.SetNumberOfRows(rowSelection.RowCount, "default");

            for (int i = rowSelection.BeginIndex, j = 0; i <= rowSelection.EndIndex; i++, j++)
            {
                var elementRow = (PhotoRowController)TablePhotos.GetRowController(j);

                await elementRow.SetElement(photos.ElementAt(i));
            }
        }

        #endregion
    }
}