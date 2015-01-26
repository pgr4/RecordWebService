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
        /// Get all Songs from the database
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<JSONArtist> Get()
        //{
        //    return MergeSongs(DatabaseSingleton.Instance.DbSongs);
        //}

        //private List<JSONArtist> MergeSongs(Table<tblSong> list)
        //{
        //    List<JSONArtist> ret = new List<JSONArtist>();
        //    foreach (var item in list.ToList())
        //    {
        //        var artist = (from i in ret
        //                      where i.Name == item.Artist
        //                      select i).ToList().FirstOrDefault();

        //        if (artist == null)
        //        {
        //            ret.Add(new JSONArtist(item.Artist, new JSONAlbum(DatabaseSingleton.Instance.GetImageData(item.Key), item.Title, item.Album)));
        //        }
        //        else
        //        {
        //            var album = (from ai in artist.Albums
        //                         where ai.Name == item.Album
        //                         select ai).ToList().FirstOrDefault();

        //            if (album == null)
        //            {
        //                artist.Albums.Add(new JSONAlbum(DatabaseSingleton.Instance.GetImageData(item.Key), item.Title, item.Album));
        //            }
        //            else
        //            {
        //                album.Songs.Add(item.Title);
        //            }
        //        }
        //    }

        //    return (from i in ret
        //            orderby i.Name descending
        //            select i).ToList();
        //}

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

        /// <summary>
        /// Add a tblAlbum and multiple tblSong entries to the database given the JsonAlbumData retrieved
        /// </summary>
        /// <param name="jad"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public List<tblSong> Post([FromBody] JsonAlbumData jad)
        {
            DatabaseSingleton.Instance.AddTblAlbum(jad.GeTblAlbum());

            int i = 0;
            foreach (var item in jad.GetTblSongs())
            {
                DatabaseSingleton.Instance.AddTblSong(item, i++);
            }

            return jad.GetTblSongs();
        }

    }
}