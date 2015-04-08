using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using RecordWebService.Models;

namespace RecordWebService.Controllers
{
    public class SongController : ApiController
    {
        /// <summary>
        /// Get all songs in the database that match the key from s
        /// TODO:THIS MAY NOT BE NEEDED SINCE THE APP PASSES THE INFORMATION OFF TO THE VIEW AFTER UPDATING THE DATABASE HOWEVER IT MAY BE NEEDED LATER ON
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public TotalAlbum Get(string key)
        {
            TotalAlbum ret = new TotalAlbum();
            var b = Convert.FromBase64String(key);
            ret.Album = DatabaseSingleton.Instance.GetTblAlbums(b);
            ret.Songs = (from item in DatabaseSingleton.Instance.DbSongs
                         where item.Key == b
                         select item.Title).ToList();
            return ret;
        }

        public void Get()
        {
            JsonAlbumData a = new JsonAlbumData();
            a.Album = "sds";
            a.Artist = "art";
            a.Key = "45141E28323C46505A64";
            a.Songs = "1,2,3,4,5,6,7,8,9,10,11";
            a.Image = "http://schemas.com";
            DatabaseSingleton.Instance.AddTblAlbum(a.GetTblAlbum());
        }

        /// <summary>
        /// Add a tblAlbum and multiple tblSong entries to the database given the JsonAlbumData retrieved
        /// </summary>
        /// <param name="jad"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public List<tblSong> Post([FromBody] JsonAlbumData jad)
        {
            DatabaseSingleton.Instance.AddTblAlbum(jad.GetTblAlbum());

            int i = 0;
            foreach (var item in jad.GetTblSongs())
            {
                DatabaseSingleton.Instance.AddTblSong(item);
            }

            return jad.GetTblSongs();
        }

    }
}