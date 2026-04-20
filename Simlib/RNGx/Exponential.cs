using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simlib.RNGx
{
    public class Exponential : RNG
    {
        private double mean;

        public Exponential(double mean)
            : base()
        {
            this.mean = mean;
        }

        public override double Next() => (0.0 - mean) * System.Math.Log(Random.NextDouble());
    }
}
