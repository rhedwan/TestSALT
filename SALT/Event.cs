using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT
{
    public sealed class Event
    {
        public Event(Node node, string type, double time, params object[] args)
        {
            Node = node;
            Type = type;
            Time = time;
            if (args != null)
                Args = args;
        }

        public object[] Args { get; }

        public string ArgsAsString()
        {
            string args = "";
            if (Args == null)
                return args;
            for (int i = 0; i < Args.Length; i++)
                args += $" { Args[i] }";
            return args;
        }

        public Node Node { get; }

        public double Time { get; }

        public string Type { get; }

        public override string ToString() => $"[Time={ Time }, Node={ Node.Name } Type={ Type } Args:{ ArgsAsString() }]";
    }
}
