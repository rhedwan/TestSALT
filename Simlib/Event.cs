using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simlib
{
    public class Event
    {
        public Event(double t, string typeOfEvent)
        {
            Time = t;
            Type = typeOfEvent;
        }

        public double Time { get; }

        public string Type { get; }

        public override string ToString() => $"[Time: { Time }, Type: { Type }]";
    }
}
