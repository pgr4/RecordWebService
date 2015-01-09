using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordWebService.Models
{
    public class Song
    {
        public byte[] Key { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public int BreakNumber { get; set; }

        public Song()
        {
            
        }

        public Song(tblSong tblSong)
        {
            Key = tblSong.Key;
            Title = tblSong.Title;
            Artist = tblSong.Artist;
            Album = tblSong.Album;
            BreakNumber = tblSong.Break_Number;
        }
    }
}