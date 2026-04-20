using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALTx.ISYS
{
    public class Edge : SALT.Edge
    {
        public Edge(Endpt x, Endpt y)
            : base(x, y)
        {
            //
        }

        public Edge(Inventory x, Product y)
            : base(new ISYS.Endpt(x.Name, x), new ISYS.Endpt(y.Name, y))
        {
            Endpt e;
            e = GetEndpt(x.Name);
            x.Add(e);
            e = GetEndpt(y.Name);
            y.Add(e);
        }

        public override string ToString() => $"[{ X }][{ Y }]";
    }
}