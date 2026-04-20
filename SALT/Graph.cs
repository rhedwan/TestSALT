using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT
{
    public class Graph
    {
        private List<Node> nodes = new List<Node>();
        private List<Edge> edges = new List<Edge>();

        private List<Event> e = new List<Event>();

        public Graph()
        {
            Stats = new Stats();
        }

        public void Add(params Edge[] edges)
        {
            for (int i = 0; i < edges.Length; i++)
                this.edges.Add(edges[i]);
        }

        public void Add(params Node[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
                this.nodes.Add(nodes[i]);
        }

        public static Clock Clock { get; set; }

        public List<Event> Events => e;

        public void GetNext()
        {
            if (e.Count == 0)
            {
                Next = null;
                return;
            }

            // order events by increasing time
            IOrderedEnumerable<SALT.Event> o = e.OrderBy(e => e.Time);

            // select event with minimum time
            SALT.Event v = o.ElementAt(0);

            // remove selected event from event list 
            e.Remove(v);

            // return selected event
            Next = v;
        }

        protected int Init()
        {
            // 0. initialize clock
            Clock = new Clock();

            // 2. get initial list of events
            Event[] t;
            for (int i = 0; i < nodes.Count; i++)
            {
                t = nodes[i].Init();
                if (t == null)
                    continue;
                for (int j = 0; j < t.Length; j++)
                    e.Add(t[j]);
            }

            return 1;
        }

        /// <summary>
        /// holds next event
        /// </summary>
        public static Event Next { get; set; }

        public void Run()
        {
            Event[] events;

            // generate all initial events from nodes
            Init(); 

            while (true)
            {
                GetNext();
                if (Next == null)
                    break;
                Clock.Now = Next.Time;
                if (Next.Type.CompareTo("end-simulation") == 0)
                    break;
                events = Next.Node.Execute();
                if (events == null)
                    continue;
                for (int i = 0; i < events.Length; i++)
                    e.Add(events[i]);
            }

            // build statitsics collection
            for (int i = 0; i < nodes.Count; i++)
                Stats.AddStat(nodes[i]);
        }

        public SALT.Stats Stats { get; set; }
    }
}