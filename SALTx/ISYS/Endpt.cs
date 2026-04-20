using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALTx.ISYS
{
    public class Endpt : SALT.Edge.Endpt
    {
        /// <summary>
        /// endpoint constructor
        /// </summary>
        /// <param name="id">name of host</param>
        /// <param name="host"></param>
        public Endpt(string id, SALT.Node host)
            : base(id, host)
        {
            //
        }

        public override object[] Get(string msg)
        {
            switch (msg)
            {
                case "name":
                    return new object[] { Twin.ID };
                default:
                    return Twin.Get(Twin.ID, msg);
            }
        }

        public override object[] Get(string twin, string msg)
        {
            object[] obj = null;

            if (twin.CompareTo(ID) == 0)
            {
                switch (ID)
                {
                    case "inventory":
                        Inventory i = (Inventory)Host;
                        switch (msg)
                        {
                            case "name-&-id":
                                obj = new object[] { i.Name, i.ID };
                                break;
                        }
                        break;
                    case "product":
                        Product p = (Product)Host;
                        switch (msg)
                        {
                            case "cumulative-demand-distribution":
                                obj = new object[] { p.CumulativeDemandDistr };
                                break;
                            case "demands":
                                obj = new object[] { p.Demand };
                                break;
                            case "id":
                                obj = new object[] { p.ID };
                                break;
                            case "levels":
                                obj = new object[] { p.Count, p.MinLvl, p.MaxLvl };
                                break;
                            case "lags":
                                obj = new object[] { p.MinLag, p.MaxLag };
                                break;
                            case "mean-interdemand-interval":
                                obj = new object[] { p.MeanInterDemand };
                                break;
                            case "name":
                                obj = new object[] { p.Name };
                                break;
                            case "name-&-id":
                                obj = new object[] { p.Name, p.ID };
                                break;
                        }
                        break;
                }
            }

            return obj;
        }

        public override string Set(string message, params object[] obj) => Twin.Set(Twin.ID, message, obj);


        public override string Set(string receiver, string message, params object[] obj)
        {
            // 0. host name is product or inventory
            if (ID.CompareTo(receiver) == 0)
            {
                switch (ID)
                {
                    case "inventory":
                        break;
                    case "product":
                        Product p = (Product)Host;
                        switch (message)
                        {
                            case "demand":
                                p.Remove((int)obj[0]);
                                return $"{ p.Count}";
                            case "order":
                                p.Add((int)obj[0]);
                                return $"{ p.Count }";
                        }
                        break;
                }

            }
            //
            return "fail";
        }

        public override string ToString() => $"Id={ ID }, hostName={ Host.Name }, hostId={ Host.ID }";
    }
}