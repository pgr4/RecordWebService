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
        //TODO:REMOVE AS NOT NEEDED
        public IEnumerable<Album> Get()
        {
            List<Album> ret = new List<Album>();
            var dt = DatabaseSingleton.Instance.DbAlbums;
            foreach (var item in dt)
            {
                ret.Add(new Album(item));
            }
            return ret;
        }

        public Album Get(string s)
        {
            var b = StaticMethods.StringToByteArray(s);
            Album ret = new Album(DatabaseSingleton.Instance.GetTblAlbums(b));
            return ret;
        }
    }
}
