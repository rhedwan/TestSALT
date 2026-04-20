using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT
{
    public abstract class RNG
    {
        private System.Random r = new Random();

        public RNG()
        {

        }

        public abstract double Next();

        public System.Random Random => r;
    }
}
