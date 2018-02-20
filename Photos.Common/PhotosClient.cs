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

            CheckStatusCode(response);

            return await DeserializeResponse<IEnumerable<Photo>>(response);
        }

        public static async Task<byte[]> GetImageData(Photo photo)
        {
            Check.IsNull(photo);

            var result = await httpClient.GetByteArrayAsync(photo.ThumbnailUrl);

            return result;
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
