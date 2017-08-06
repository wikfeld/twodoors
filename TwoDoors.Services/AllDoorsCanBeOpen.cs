using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDoors.Services
{
    public class AllDoorsCanBeOpen : IDoorAccessControl
    {
        public bool CanOpen(int doorId, string doorToken)
        {
            return true;
        }
    }
}
