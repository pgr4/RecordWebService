using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecordWebService.Models;

namespace RecordWebService.Controllers
{
    public class UpdateController : ApiController
    {
        /// <summary>
        /// Update the database
        /// TODO: Add a new Update Controller
        /// </summary>
        [System.Web.Http.HttpGet]
        public void Get()
        {
            DatabaseSingleton.Instance.RefreshDatabaseTables();
        } 
    }
}
