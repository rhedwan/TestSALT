using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT
{
    public class Daemon
    {
        /// <summary>
        /// adds a Random Number Generator (RNG) to Random
        /// </summary>
        /// <param name="rng">random number generator</param>
        /// <param name="name">name of object that demands a stream of random numbers</param>
        /// <param name="id">identifier of object</param>
        public static void AddRNG(RNG rng, string name, int id)
        {
            Indexer.Add(new object[] { name, id });
            Random.Add(rng);
        }

        /// <summary>
        /// returns an RNG with specified index
        /// </summary>
        /// <param name="index">name and id of object</param>
        /// <returns></returns>
        public static RNG GetRNG(params object[] index)
        {
            if (Indexer.Count == 0)
                return null;
            for (int i = 0; i < Indexer.Count; i++)
                if ((((string)index[0]).CompareTo((string)Indexer[i][0]) == 0) && (((int)index[1]) == ((int)Indexer[i][1])))
                    return Random[i];
            return null;
        }

        private static List<object[]> Indexer { get; } = new List<object[]>();

        private static List<RNG> Random { get; } = new List<RNG>();
    }
}
