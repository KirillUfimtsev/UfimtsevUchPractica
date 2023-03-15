
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UfimtsevUchPractica.Entities;

namespace UfimtsevUchPractica
{
    public class FullOrder
    {
        public FullOrder(int OrderID, int OrderStatusID, DateTime OrderDeliveryDate, int PickUpPointID, DateTime OrderDate, int UserID)
        {
            this.orderID = orderID;
            this.orderStatusID = orderStatusID;
            this.orderDeliveryDate = orderDeliveryDate;
            this.pickUpPointID = pickUpPointID;
            this.orderDate = orderDate;
            this.userID = userID;
        }
        public int orderID { get; set; }
        public int orderStatusID { get; set; }
        public DateTime orderDeliveryDate { get; set; }
        public int pickUpPointID { get; set; }
        public DateTime orderDate { get; set; }
        public int userID { get; set; }


        public string Receiver
        {
            get
            {
                var order = UchPracticaEntities.GetContext().Order.Where(p => p.OrderID == orderID).First();
                var client = UchPracticaEntities.GetContext().User.Where(p=>p.UserID == order.UserID).First();
                var fio = client.UserSurname + " " + client.UserName + " " + client.UserPatronymic;
                return fio;
            }
        }
        //public string[] Cost
        //{

        //    get
        //    {
        //        List<string> cost = new List<string>();
        //        for (int i = 0; i < Orders.Count; i++)
        //        {
        //            string orderPrice = "0";
        //            var carsInOrders = UchPracticaEntities.GetContext().OrderCar.Where(p => p.OrderID == Orders[i].OrderID).ToList();
        //            for (int k = 0; k < carsInOrders.Count; k++)
        //            {
        //                orderPrice = (Convert.ToInt64(orderPrice) + carsInOrders[k].Car.Price).ToString();
        //            }
        //            cost.Add(orderPrice);
        //        }
        //        return cost.ToArray();

        //    }
        //}
    }


}
