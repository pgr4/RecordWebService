using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordWebService.Models
{
    public class JsonAlbumData
    {
        public string Key { get; set; }
        public string Songs { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }

        public JsonAlbumData()
        {

        }

        public List<tblSong> GetTblSongs()
        {
            var key = StaticMethods.StringToByteArray(Key);

            List<tblSong> ret = new List<tblSong>();
            int i = 0;
            var splitList = Songs.Split(',');
            foreach (var item in splitList)
            {
                int start, end;
                if (i == 0)
                {
                    start = int.MinValue;
                    end = key[0];
                }
                else if (i == splitList.Count() - 1)
                {
                    start = key[i - 1];
                    end = int.MaxValue;
                }
                else
                {
                    start = key[i - 1];
                    end = key[i];
                }

                ret.Add(new tblSong()
                {
                    Album = Album,
                    Artist = Artist,
                    Break_Number = i++,
                    Key = key,
                    Title = item,
                    Break_Location_Start = start,
                    Break_Location_End = end
                });
            }

            return ret;
        }

        public tblAlbum GeTblAlbum()
        {
            var key = StaticMethods.StringToByteArray(Key);
            var s = Songs.Split(',');
            tblAlbum ret = new tblAlbum()
            {
                Album = Album,
                Artist = Artist,
                Breaks = s.Count(),
                Calculated = 1,
                Key = key
                //TODO:Add Image
                //Image = 
            };
            return ret;
        }
    }
}