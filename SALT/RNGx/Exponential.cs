using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT.RNGx
{
    public class Exponential : RNG
    {
        public Exponential(double mean)
            : base()
        {
            Mean = mean;
        }

        public double Mean { get; }

        public override double Next() => (0.0 - Mean) * System.Math.Log(Random.NextDouble());
    }
}
