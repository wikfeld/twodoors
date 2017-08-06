using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDoors.Models
{
    public interface ITimeProvider
    {
        DateTime CurrentTime();
    }
}
