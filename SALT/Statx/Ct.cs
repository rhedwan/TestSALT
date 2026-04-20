using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT.Statx
{
    /// <summary>
    /// continuous time statistic
    /// </summary>
    public class Ct : Stat
    {
        private List<double> d = new List<double>(); // data
        private List<double> t = new List<double>(); // discrete time

        private double auc = 0.0;

        public Ct(string name)
            : base(name)
        {

        }

        public Ct(string name, double bias)
            : base(name)
        {
            Bias = bias;
        }

        public Ct(string name, double bias, double data, double time)
            : base(name)
        {
            Bias = bias;
            Init(data, time);
        }

        /// <summary>
        /// add area under curve
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="now">now</param>
        public void Add(double data, double now)
        {
            auc += d.Last() * (now - t.Last());
            d.Add(Math.Max(Bias * data, 0));
            t.Add(now);
        }

        public double AUC => auc;

        public double Bias { get; set; }

        public override System.Collections.ObjectModel.ReadOnlyCollection<double> Data => d.AsReadOnly();

        /// <summary>
        /// initialize
        /// </summary>
        /// <param name="data">initial data</param>
        /// <param name="time">initial time</param>
        public Ct Init(double data, double time)
        {
            d.Add(Math.Max(Bias * data, 0));
            t.Add(time);
            return this;
        }

        public override double Mean => auc / TotalTime;

        /// <summary>
        /// discrete time
        /// </summary>
        public override System.Collections.ObjectModel.ReadOnlyCollection<double> Time => t.AsReadOnly();

        public override string ToString()
        {
            string dt = "";

            dt += $"\n\tName={Name}";
            dt += $"\n\tAUC={AUC}";
            dt += $"\n\tBias={Bias}";
            dt += $"\n\tMean={Mean}";
            dt += $"\n\tTotal Simulated Time={TotalTime}";

            for (int i = 0; i < d.Count; i++)
                dt += $"\n\tSample[{i:D3}]: t={t[i],7:0.###} {d[i],7:0.####}";

            return dt;
        }

        public double TotalTime => t.Last() - t.First();
    }
}