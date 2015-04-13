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
            //var b = DatabaseSingleton.StringToKey(key);
            //var b = Convert.FromBase64String(key);
            ret.Album = DatabaseSingleton.Instance.GetTblAlbums(key);
            ret.Songs = (from item in DatabaseSingleton.Instance.DbSongs
                         where item.Key == key 
                         select item.Title).ToList();
            return ret;
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