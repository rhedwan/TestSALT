using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simlib
{
    public class Events
    {
        private List<Event> events;

        public Events()
        {
            events = new List<Event>();
        }

        public void Add(Event e) => events.Add(e);

        public List<Event> List => events;

        public Simlib.Event Next()
        {
            if (events.Count == 0)
                return null;
            // order events by increasing time
            IOrderedEnumerable<Simlib.Event> o = events.OrderBy(e => e.Time);
            // select event with minimum time
            Simlib.Event v = o.ElementAt(0);
            // remove selected event from event list 
            events.Remove(v);
            // return selected event
            return v;
        }
    }
}
