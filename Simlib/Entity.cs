using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simlib
{
    public class Entity
    {
        private static int id = 0;

        public Entity(int id, double tofA)
        {
            ID = id;
            TofA = tofA;
        }

        public int ID { get; }

        public static int NextID => id++;

        /// <summary>
        /// time of Arrival
        /// </summary>
        public double TofA { get; }
    }
}
