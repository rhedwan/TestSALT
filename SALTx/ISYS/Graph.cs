using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALTx.ISYS
{
    public class Graph : SALT.Graph
    {
        // initial_inv_level, num_months, num_policies, num_values_demand, mean_interdemand, setup_cost, incremental_cost, holding_cost,
        // shortage_cost, minlag, maxlag

        private int numberOfMonths;

        /// <summary>
        /// construct graph with a single product [Simlib example]
        /// </summary>
        /// <param name="initialInventoryLevel"></param>
        /// <param name="numberOfMonths"></param>
        /// <param name="demand"></param>
        /// <param name="meanInterDemand"></param>
        /// <param name="setupCost"></param>
        /// <param name="incrementalCost"></param>
        /// <param name="holdingCost"></param>
        /// <param name="shortageCost"></param>
        /// <param name="maxlag"></param>
        /// <param name="minlag"></param>
        public Graph(int initialInventoryLevel, int numberOfMonths, object[][] demand, double meanInterDemand,
            double setupCost, double incrementalCost, double holdingCost, double shortageCost, double maxlag, double minlag, int maxlvl, int minlvl)
            : base()
        {
            // 0. set graph parameters
            this.numberOfMonths = numberOfMonths;

            // 1. construct graph
            Inventory inventory = new Inventory(0, "inventory");
            Product product = new Product(0, "product", demand, meanInterDemand, setupCost, incrementalCost,
                holdingCost, shortageCost, initialInventoryLevel, maxlag, minlag, maxlvl, minlvl);
            SALT.Edge edge = new Edge(inventory, product);

            // add inventory and product
            Add(inventory, product);

            // add connecting edge
            Add(edge);

            // 2. initialize terminating event
            Events.Add(new SALT.Event(null, "end-simulation", numberOfMonths));
        }

        /// <summary>
        /// construct graph with single inventory and multiple products
        /// </summary>
        /// <param name="products">list of products in inventory</param>
        public Graph(List<Product> products)
            : base()
        {
            numberOfMonths = 5;

            Inventory inventory = new Inventory(0, "inventory");
            Add(inventory);
            Add(products.ToArray());

            // construct and add edges
            for (int i = 0; i < products.Count; i++)
                Add(new Edge(inventory, products[i]));

            // add [end simulation] event
            Events.Add(new SALT.Event(null, "end-simulation", numberOfMonths));
        }
    }
}