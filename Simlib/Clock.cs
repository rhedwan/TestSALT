using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simlib
{
    public class Clock
    {
        public Clock(double t)
        {
            Now = t;
        }

        public double Now { get; set; }

        public double TimeOfLastEvent { get; set; }

    }
}