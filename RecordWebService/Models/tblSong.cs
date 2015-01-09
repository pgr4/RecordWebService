﻿using System.Data.Linq.Mapping;

namespace RecordWebService.Models
{
    [Table(Name = "tblSong")]
    public class tblSong
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column(Name = "Key")]
        public byte[] Key { get; set; }
        [Column(Name = "Title")]
        public string Title { get; set; }
        [Column(Name = "Artist")]
        public string Artist { get; set; }
        [Column(Name = "Album")]
        public string Album { get; set; }
        [Column(Name = "Break_Number")]
        public int Break_Number { get; set; }
    }
}