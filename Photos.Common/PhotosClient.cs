#region Using

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Photos.Common.Models;
using Photos.Common.Helpers;

#endregion

namespace Photos.Common
{
    public static class PhotosClient
    {
        #region Fields

        private static HttpClient httpClient;

        #endregion

        #region Constructor

        static PhotosClient()
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };
        }

        #endregion

        #region Methods (Public)

        public static async Task<IEnumerable<Photo>> GetByAlbumId(int albumId)
        {
            var response = await httpClient.GetAsync($"photos?albumId={albumId}");

            var photoCollection = new List<Photo>();

            try
            {
                CheckStatusCode(response);

                photoCollection = await DeserializeResponse<List<Photo>>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return photoCollection;
        }

        public static async Task<byte[]> GetImageData(Photo photo)
        {
            var imageData = new byte[0];

            try
            {
                Check.IsNull(photo);

                imageData = await httpClient.GetByteArrayAsync(photo.ThumbnailUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return imageData;
        }

        #endregion

        #region Methods (Private)

        private static void CheckStatusCode(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Unexpected status code: {response.StatusCode}");
            }
        }

        private static async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var jsonString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        #endregion
    }
}
