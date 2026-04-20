using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT
{
    public class Clock
    {
        private double now;
        private double tofLE;

        public Clock()
        {
            now = 0.0;
            tofLE = 0.0;
        }

        /// <summary>
        /// gets and sets current time
        /// </summary>
        public double Now
        {
            get
            {
                return now;
            }
            set
            {
                tofLE = now;
                now = value;
            }
        }

        public double TimeOfLastEvent => tofLE;

    }
}
