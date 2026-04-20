using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT
{
    public abstract class Stat
    {
        public Stat(string name)
        {
            Name = name;
        }

        public abstract System.Collections.ObjectModel.ReadOnlyCollection<double> Data { get; }

        public abstract double Mean { get; }

        public string Name { get; }

        public abstract System.Collections.ObjectModel.ReadOnlyCollection<double> Time { get; }
    }
}
