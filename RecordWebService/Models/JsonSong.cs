using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace RecordWebService.Models
{
    public class JsonAlbumData
    {
        public string Key { get; set; }
        public string Songs { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Image { get; set; }

        public JsonAlbumData()
        {

        }

        public List<tblSong> GetTblSongs()
        {
            List<tblSong> ret = new List<tblSong>();
            try
            {
                var bytesKey = StaticMethods.StringToByteArray(Key);

                int i = 0;

                var splitList = Songs.Split(',');

                foreach (var item in splitList)
                {
                    int start, end;
                    if (i == 0)
                    {
                        start = int.MinValue;
                        end = bytesKey[0];
                    }
                    else if (i == splitList.Count() - 1)
                    {
                        start = bytesKey[i - 1];
                        end = int.MaxValue;
                    }
                    else
                    {
                        start = bytesKey[i - 1];
                        end = bytesKey[i];
                    }

                    ret.Add(new tblSong()
                    {
                        Album = Album,
                        Artist = Artist,
                        Break_Number = i++,
                        Key = bytesKey,
                        Title = item,
                        Break_Location_Start = start,
                        Break_Location_End = end
                    });
                }
            }
            catch (Exception)
            {
                return ret;
            }

            return ret;
        }

        public tblAlbum GetTblAlbum()
        {
            var bytesKey = StaticMethods.StringToByteArray(Key);

            var s = Songs.Split(',');
            tblAlbum ret = new tblAlbum()
            {
                Album = Album,
                Artist = Artist,
                Breaks = s.Count() - 1,
                Calculated = 1,
                Key = bytesKey,
                Image = DownloadRemoteImageFile(Image)
            };
            return ret;
        }

        private static byte[] DownloadRemoteImageFile(string uri)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();

            // Check that the remote file was found. The ContentType
            // check is performed since a request for a non-existent
            // image file might be redirected to a 404-page, which would
            // yield the StatusCode "OK", even though the image was not
            // found.
            if ((response.StatusCode == HttpStatusCode.OK ||
                 response.StatusCode == HttpStatusCode.Moved ||
                 response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {

                // if the remote file was found, download oit
                using (Stream inputStream = response.GetResponseStream())
                {
                    return ReadFully(inputStream);
                }
            }
            else
            {
                return null;
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

    }
}