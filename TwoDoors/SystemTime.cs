using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwoDoors.Models;

namespace TwoDoors
{
    public class SystemTime : ITimeProvider
    {
        public DateTime CurrentTime()
        {
            return DateTime.Now;
        }
    }
}