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
        public IEnumerable<Song> Get()
        {
            return MergeSongs(DatabaseSingleton.Instance.DbSongs);
        }

        private List<Song> MergeSongs(Table<tblSong> list)
        {
            List<Song> ret =new List<Song>();
            foreach (var item in list)
            {
                var found = (from i in ret
                    where i.Album == item.Album
                    where i.Artist == item.Artist
                    select i).FirstOrDefault();

                if (found == null)
                {
                    ret.Add(new Song(item.Artist,item.Album,item.Title));
                }
                else
                {
                    found.Titles.Add(item.Title);
                }
            }

            return ret;
        }

        /// <summary>
        /// Get all songs in the database that match the key from s
        /// TODO:THIS MAY NOT BE NEEDED SINCE THE APP PASSES THE INFORMATION OFF TO THE VIEW AFTER UPDATING THE DATABASE HOWEVER IT MAY BE NEEDED LATER ON
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public Song Get(string s)
        {
            Song ret = null;
            var b = StaticMethods.StringToByteArray(s);
            foreach (var item in DatabaseSingleton.Instance.GetTblSongs(b))
            {
                if (ret == null)
                {
                    ret = new Song(item.Artist,item.Album,item.Title);
                }
                else
                {
                    ret.Titles.Add(item.Title);
                }
            }
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
                DatabaseSingleton.Instance.AddTblSong(item,i++);
            }

            return jad.GetTblSongs();
        }

    }
}