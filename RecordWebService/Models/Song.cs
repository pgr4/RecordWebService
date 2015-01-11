using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordWebService.Models
{
    public class Song
    {
        public List<string> Titles { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }

        public Song()
        {
            
        }

        public Song(string artist, string album, string title)
        {
            Artist = artist;
            Album = album;
            Titles = new List<string>();
            Titles.Add(title);
        }

    }
}