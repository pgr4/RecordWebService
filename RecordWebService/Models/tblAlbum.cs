﻿using System.Data.Linq.Mapping;

namespace RecordWebService.Models
{
    [Table(Name = "tblAlbum")]
    public class tblAlbum
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column(Name = "Key")]
        public string Key { get; set; }
        [Column(Name = "Album")]
        public string Album { get; set; }
        [Column(Name = "Artist")]
        public string Artist { get; set; }
        [Column(Name = "Calculated")]
        public int Calculated { get; set; }
        [Column(Name = "Breaks")]
        public int Breaks { get; set; }
        [Column(Name = "Image")]
        public byte[] Image { get; set; }
    }
}
