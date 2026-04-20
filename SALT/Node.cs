using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT
{
    public abstract class Node
    {
        private static int[] id = null;
        private static string[] names = null;

        private List<Edge.Endpt> endpoints = new List<Edge.Endpt>();

        public Node(int id, string name, Statx.Ct[] ctStats, Statx.Dt[] dtStats)
        {
            CtStats = ctStats;
            DtStats = dtStats;
            ID = id;
            Name = name;
        }

        public void Add(Edge.Endpt endpoint)
        {
            if (!endpoint.Host.Equals(this))
                throw new Exception();
            endpoints.Add(endpoint);
        }

        /// <summary>
        /// create multiple identifiers for entities in simulation
        /// </summary>
        /// <param name="nofE"></param>
        public static void CreateNodeID(params string[] entityNames)
        {
            int nofE = entityNames.Length;
            names = entityNames;
            id = new int[nofE];
            for (int i = 0; i < nofE; i++)
                id[i] = 0;
        }

        public Statx.Ct[] CtStats { get; }

        public Statx.Dt[] DtStats { get; }

        public abstract Event[] Execute();

        public abstract SALT.Stat[] FinalizeStats();

        public Edge.Endpt[] GetEndpts(string name)
        {
            List<Edge.Endpt> endpts = new List<Edge.Endpt>();

            object[] obj;
            for (int i = 0; i < endpoints.Count; i++)
            {
                obj = endpoints[i].Get("name");
                if (((string)obj[0]).CompareTo(name) == 0)
                   endpts.Add(endpoints[i]);
            }
            //
            if (endpts.Count == 0)
                return null;
            return endpts.ToArray();
        }

        public Edge.Endpt GetEndpt(string name, int id)
        {
            object[] obj;
            for (int i = 0; i < endpoints.Count; i++)
            {
                obj = endpoints[i].Get("name");
                if (((string)obj[0]).CompareTo(name) == 0)
                {
                    obj = endpoints[i].Get("id");
                    if (id == (int)obj[0])
                        return endpoints[i];
                }
            }
            //
            return null;
        }

        public int ID { get; }

        public abstract Event[] Init();

        public string Name { get; }

        /// <summary>
        /// get next object id using entity identifier
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public static int NextID(int entityID) => id[entityID]++;

        /// <summary>
        /// get next object id using name of entity class
        /// </summary>
        /// <param name="name">entity-name</param>
        /// <returns></returns>
        public static int NextID(string name)
        {
            int? idx = null;
            for (int i = 0; i < names.Length; i++)
            {
                if (name.CompareTo(names[i]) == 0)
                {
                    idx = i;
                    break;
                }
            }

            if (idx == null)
                throw new Exception();
            return id[idx.Value]++;
        }
    }
}
