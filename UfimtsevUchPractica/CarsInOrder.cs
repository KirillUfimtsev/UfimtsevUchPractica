using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UfimtsevUchPractica
{
    public class CarsInOrder
    {
        public CarsInOrder( string mark, string model, string price, DateTime orderDate, DateTime orderDeliveryDate,int orderID)
        {
            this.mark = mark;
            this.model = model;
            this.price = price;
            this.orderDate = orderDate;
            this.orderDeliveryDate = orderDeliveryDate;
            this.orderID= orderID;
        }

        public string mark { get; }
        public string model { get; }
        public string price { get; }
        public DateTime orderDate { get; }
        public DateTime orderDeliveryDate { get; }
        public int orderID { get; }
    }
}
