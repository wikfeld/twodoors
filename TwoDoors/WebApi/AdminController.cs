using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TwoDoors.Models;

namespace TwoDoors.WebApi
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private IAccessLogRepository _accessLog;

        public AdminController(IAccessLogRepository accessLog)
        {
            _accessLog = accessLog;
        }

        [HttpGet]
        [Route("reset")]
        public IHttpActionResult Reset()
        {
            // clear the historical events
            _accessLog.Clear();
            return Ok();
        }
    }
}