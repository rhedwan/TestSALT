using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT
{
    public class Stats
    {
        private List<Stat[]> stats = new List<Stat[]>();
        private string report = "";
        private int c = 0;

        public Stats()
        {

        }

        public void AddStat(Node node)
        {
            report += $"\n\n\tNODE[{c++}]\n\t{node}";

            SALT.Stat[] statx = node.FinalizeStats();
            if (statx == null)
                return;

            stats.Add(statx);
            for (int i = 0; i < statx.Length; i++)
                report += $"\n\t{statx[i]}";
        }

        /// <summary>
        /// return the mean of a data collection
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static double Mean(double[] collection)
        {
            double m = 0.0;
            int n = collection.Length;
            for (int i = 0; i < n; i++)
                m += collection[i];
            return m / n;
        }

        /// <summary>
        /// return the median of a data collection
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static double Median(double[] collection)
        {
            return 0.0;
        }

        /// <summary>
        /// returns the mode of a data collection
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static double Mode(double[] collection)
        {
            return 0.0;
        }

        public override string ToString() => report;
    }
}
