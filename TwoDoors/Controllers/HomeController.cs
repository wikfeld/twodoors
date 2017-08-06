using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TwoDoors.Models;
using TwoDoors.Services;

namespace TwoDoors.Controllers
{
    public class HomeController : Controller
    {
        private IDoorRepository _doors;
        private IAccessLogRepository _history;
        private IDoorAccessControl _access;

        public HomeController(IDoorRepository doors, IAccessLogRepository history, IDoorAccessControl access)
        {
            _doors = doors;
            _history = history;
            _access = access;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Doors()
        {
            return View(new[] { _doors.Get(1), _doors.Get(2) });
        }

        public ActionResult History()
        {
            return View(_history.GetAll(1).Concat(_history.GetAll(2)).OrderByDescending(x=>x.AccessTimestamp));
        }

        public async Task<ActionResult> OpenDoor(int doorId, string secret)
        {
            var client = new HttpClient();
            var url = string.Format("{0}://{1}{2}api/doorman/door/{3}/open/{4}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"), doorId, secret);
            var result = await client.GetAsync(url);

            return RedirectToAction("Doors");
        }
    }
}