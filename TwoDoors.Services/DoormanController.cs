using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TwoDoors.Models;

namespace TwoDoors.Services
{
    [RoutePrefix("api/doorman")]
    public class DoormanController : ApiController
    {
        private IDoorAccessControl _accessControl;

        public DoormanController(IDoorAccessControl accessControl)
        {
            _accessControl = accessControl;
        } 

        [HttpGet]
        [Route("open/{doorId}/token/{secret}")]
        public IHttpActionResult Open(int doorId, string secret)
        {
            var result = _accessControl.CanOpen(doorId, secret);
            return Ok(result);
        }
    }
}
