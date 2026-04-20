using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT
{
    public class DES
    {
        public DES()
        {

        }

        public string Run(Graph g)
        {
            string log = "";

            g.Run();
            log += g.Stats.ToString();

            return log;
        }
    }
}