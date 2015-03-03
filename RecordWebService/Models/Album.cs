using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordWebService.Models
{
    public class Album
    {
        public byte[] Key { get; set; }
        public string Artist { get; set; }
        public string Name { get; set; }
        public int Calculated { get; set; }
        public int Breaks { get; set; }
        public byte[] Image { get; set; }
        public List<string> Songs { get; set; }

        public Album()
        {
            
        }

        public Album(tblAlbum tblAlbum)
        {
            Key = tblAlbum.Key;
            Name = tblAlbum.Album;
            Artist = tblAlbum.Artist;
            Breaks = tblAlbum.Breaks;
            Image = tblAlbum.Image;
            Songs = new List<string>();
        }
    }
}