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
        /// <summary>
        /// Get an album from the database matching the key s
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public Album Get(string s)
        {
            var b = StaticMethods.StringToByteArray(s);
            Album ret = new Album(DatabaseSingleton.Instance.GetTblAlbums(b));
            return ret;
        }
    }
}
