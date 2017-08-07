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
        private IHistoricalEventsRepository _events;

        public DoormanController(IDoorAccessControl accessControl, IHistoricalEventsRepository events)
        {
            _accessControl = accessControl;
            _events = events;
        } 

        [HttpGet]
        [Route("door/{doorId}/open/{secret}")]
        public IHttpActionResult Open(int doorId, string secret)
        {
            // perform access control
            var result = _accessControl.CanOpen(doorId, secret);
            // open the door           
            
            // log the access
            _events.Add(doorId, result);
            return Ok(result);
        }

        [HttpGet]
        [Route("door/{doorId}/events")]
        public IHttpActionResult ViewEvents(int doorId)
        {
            return Ok(_events.GetAll(doorId));
        }
    }
}
