using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT
{
    public class Edge
    {
        private Endpt x;
        private Endpt y;

        public Edge(Endpt x, Endpt y)
        {
            this.x = x;
            this.y = y;
            this.x.Twin = this.y;
            this.y.Twin = this.x;
        }

        public Node[] GetNodes()
        {
            return new Node[] { x.Host, y.Host };
        }

        public Endpt GetEndpt(string id)
        {
            if (x.ID.CompareTo(id) == 0)
                return x;
            if (y.ID.CompareTo(id) == 0)
                return y;
            return null;
        }

        public Endpt X => x;

        public Endpt Y => y;

        public abstract class Endpt
        {
            public Endpt(string id, Node host)
            {
                Host = host;
                ID = id;
            }

            /// <summary>
            /// get message by host
            /// </summary>
            /// <param name="msg"></param>
            /// <returns></returns>
            public abstract object[] Get(string msg);

            /// <summary>
            /// get message by endpt
            /// </summary>
            /// <param name="receiver"></param>
            /// <param name="msg"></param>
            /// <returns></returns>
            public abstract object[] Get(string receiver, string msg);

            public Node Host { get; }

            public string ID { get; }

            /// <summary>
            /// set message and objects by host
            /// </summary>
            /// <param name="message">message on objects to set</param>
            /// <param name="obj">objects to set</param>
            /// <returns></returns>
            public abstract string Set(string message, params object[] obj);

            /// <summary>
            /// set message by endpoint
            /// </summary>
            /// <param name="receiver">receiver-endpoint</param>
            /// <param name="message">message on objects to set</param>
            /// <param name="obj">objects to set</param>
            /// <returns></returns>
            public abstract string Set(string receiver, string message, params object[] obj);

            public Endpt Twin { get; set; }
        }
    }
}
