using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT.RNGx
{
    public class Uniform : RNG
    {
        private double d;

        public Uniform(double min, double max)
            : base()
        {
            if (max <= min)
                throw new Exception();
            d = max - min;
            Min = min;
            Max = max;
        }

        public double Min { get; }

        public double Max { get; }

        public override double Next() => Min + (Random.NextDouble() * d);
    }
}
