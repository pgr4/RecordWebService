using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordWebService.Models
{
    public class JSONAlbum
    {
        public byte[] Image;
        public List<string> Songs;
        public string Name;

        public JSONAlbum()
        {

        }

        public JSONAlbum(byte[] image, string song, string name)
        {
            Name = name;
            Image = image;
            Songs = new List<string>();
            Songs.Add(song);
        }
    }
}