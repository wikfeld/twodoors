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
        private IAccessLogRepository _accessLog;

        public DoormanController(IDoorAccessControl accessControl, IAccessLogRepository accessLog)
        {
            _accessControl = accessControl;
            _accessLog = accessLog;
        } 

        [HttpGet]
        [Route("door/{doorId}/open/{secret}")]
        public IHttpActionResult Open(int doorId, string secret)
        {
            var result = _accessControl.CanOpen(doorId, secret);
            _accessLog.Add(doorId, result);
            return Ok(result);
        }

        [HttpGet]
        [Route("door/{doorId}/log")]
        public IHttpActionResult ViewLog(int doorId)
        {
            return Ok(_accessLog.GetAll(doorId));
        }
    }
}
