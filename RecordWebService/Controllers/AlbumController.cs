using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecordWebService.Models;

namespace RecordWebService.Controllers
{
    public class AlbumController : ApiController
    {
        [HttpGet]
        public List<Album> GetAll(string artist)
        {
            var ret = new List<Album>();

            var lst = (from item in DatabaseSingleton.Instance.DbAlbums
                       where item.Artist == artist
                       select item).ToList();

            foreach (var tblAlbum in lst)
            {
                ret.Add(new Album(tblAlbum));
            }

            return ret.OrderBy(w=>w.Name).ToList();
        }

        /// <summary>
        /// Get an album from the database matching the key s
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpGet]
        public Album Get(string s)
        {
            tblAlbum album = DatabaseSingleton.Instance.TryGetTblAlbums(s);
            if (album == null)
            {
                return null;
            }
            else
            {
                Album ret = new Album(album);
                ret.Songs = DatabaseSingleton.Instance.GetSongNames(album.Key);
                return ret;
            }
        }
    }
}
