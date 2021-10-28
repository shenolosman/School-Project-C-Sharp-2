using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class RealTime : ITime
    {
        public DateTime Today()
        {
            return DateTime.Today;
        }
    }
}
