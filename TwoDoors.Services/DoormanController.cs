using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TwoDoors.Services
{
    [RoutePrefix("api/doorman")]
    public class DoormanController : ApiController
    {
        [HttpGet]
        [Route("test")]
        public IHttpActionResult Get()
        {
            return Ok(42);
        }
    }
}
