using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simlib
{
    public abstract class DES
    {
        public DES()
        {

        }

        /// <summary>
        /// return 0 if it fails
        /// </summary>
        /// <returns></returns>
        public abstract int Init();

        /// <summary>
        /// returns 0 if no event
        /// </summary>
        /// <returns></returns>
        public abstract int Next();
    }
}
