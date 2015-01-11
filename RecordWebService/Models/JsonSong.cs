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
            foreach (var item in Songs.Split(','))
            {
                ret.Add(new tblSong()
                {
                    Album = Album,
                    Artist = Artist,
                    Break_Number = i++,
                    Key = key,
                    Title = item
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