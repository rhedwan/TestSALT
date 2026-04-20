using SALT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALTx.CPUS
{
    public class Server : SALT.Node
    {
        public Server(int id, string name, SALT.Statx.Ct[] ctStats, SALT.Statx.Dt[] dtStats)
            : base(id, name, ctStats, dtStats)
        {

        }

        public override Event[] Execute()
        {
            throw new NotImplementedException();
        }

        public override Stat[] FinalizeStats()
        {
            throw new NotImplementedException();
        }

        public override Event[] Init()
        {
            throw new NotImplementedException();
        }
    }
}
