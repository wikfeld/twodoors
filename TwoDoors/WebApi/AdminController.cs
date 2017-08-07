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
        private IHistoricalEventsRepository _events;

        public AdminController(IHistoricalEventsRepository events)
        {
            _events = events;
        }

        [HttpGet]
        [Route("reset")]
        public IHttpActionResult Reset()
        {
            // clear the historical events
            _events.Clear();
            return Ok();
        }
    }
}