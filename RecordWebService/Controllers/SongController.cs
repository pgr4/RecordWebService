using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using RecordWebService.Models;

namespace RecordWebService.Controllers
{
    public class SongController : ApiController
    {
        public IEnumerable<Song> Get()
        {
            List<Song> ret = new List<Song>();
            var dt = DatabaseSingleton.Instance.DbSongs;
            foreach (var item in dt)
            {
                ret.Add(new Song(item));
            }
            return ret;
        }

        public IEnumerable<Song> Get(string s)
        {
            List<Song> ret = new List<Song>();
            var b = StaticMethods.StringToByteArray(s);
            foreach (var item in DatabaseSingleton.Instance.GetTblSongs(b))
            {
                ret.Add(new Song(item));
            }
            return ret;
        }

        [System.Web.Http.HttpPost]
        public List<tblSong> Post([FromBody] JsonSong t)
        {
            DatabaseSingleton.Instance.AddTblAlbum(t.GeTblAlbum());

            int i = 0;
            foreach (var item in t.GetTblSongs())
            {
                DatabaseSingleton.Instance.AddTblSong(item,i++);
            }

            return t.GetTblSongs();
        }

    }
}