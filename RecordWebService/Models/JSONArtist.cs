using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordWebService.Models
{
    public class JSONArtist
    {
        public List<JSONAlbum> Albums { get; set; }
        public string Name { get; set; }

        public JSONArtist()
        {
            
        }

        public JSONArtist(string name,JSONAlbum album)
        {
            Name = name;
            Albums = new List<JSONAlbum>();
            Albums.Add(album);
        }

    }
}