using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT.Statx
{
    public class Dt : Stat
    {
        private List<double> d = new List<double>(); // data
        private List<double> t = new List<double>(); // discrete time

        public Dt(string name)
            : base(name)
        {

        }

        public void Add(double data, double time)
        {
            d.Add(data);
            t.Add(time);
        }

        public override System.Collections.ObjectModel.ReadOnlyCollection<double> Data => d.AsReadOnly();

        public override double Mean => Stats.Mean(d.ToArray());

        public override System.Collections.ObjectModel.ReadOnlyCollection<double> Time => t.AsReadOnly();

        public override string ToString()
        {
            string dt = "";
            for (int i = 0; i < d.Count; i++)
                dt += $"\n\tSample[{i:D3}]: t={t[i],8:F3} {d[i],8:F4}";
            dt += $"\n\tMean={Mean}";
            dt += $"\n\tName={Name}";

            return dt;
        }
    }
}
