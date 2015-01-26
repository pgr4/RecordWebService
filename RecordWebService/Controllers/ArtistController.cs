using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecordWebService.Models;

namespace RecordWebService.Controllers
{
    public class ArtistController : ApiController
    {
        public List<string> Get()
        {
            List<string> ret = new List<string>();
            foreach (var item in DatabaseSingleton.Instance.DbAlbums.ToList())
            {
                var artist = (from i in ret
                    where i == item.Artist
                    select i).ToList().FirstOrDefault();

                if (artist == null)
                {
                    ret.Add(item.Artist);
                }
            }
            return ret.OrderBy(q=>q).ToList();
        }
    }
}
