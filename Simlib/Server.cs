using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simlib
{
    public enum Status { BUSY, IDLE }

    public class Server
    {
        public Server()
        {

        }

        public Status Status { get; set; }
    }
}
