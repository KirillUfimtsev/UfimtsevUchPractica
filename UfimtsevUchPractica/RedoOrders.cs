using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UfimtsevUchPractica.Entities;

namespace UfimtsevUchPractica
{
    public class RedoOrders
        {
        public RedoOrders(int OrderID, int OrderStatusID, DateTime OrderDeliveryDate, int PickUpPointID, DateTime OrderDate, int UserID, List<OrderCar> OrderCar)
        {
        this.orderID = OrderID;
        this.orderStatusID = OrderStatusID;
        this.orderDeliveryDate = OrderDeliveryDate;
        this.pickUpPointID = PickUpPointID;
        this.orderDate = OrderDate;
        this.userID = UserID;
        this.orderCar = OrderCar;
        }

        public int orderID { get; set; }
        public int orderStatusID { get; set; }
        public DateTime orderDeliveryDate { get; set; }
        public int pickUpPointID { get; set; }
        public DateTime orderDate { get; set; }
        public int userID { get; set; }

        public List<OrderCar> orderCar { get; set; }

        public List<string> Mark
        {
            get
            {
                List<string> marks= new List<string>();

                foreach(OrderCar cars in orderCar)
                {
                    var car = UchPracticaEntities.GetContext().Car.Where(p => p.CarID == cars.CarID).First();
                    var model = UchPracticaEntities.GetContext().Model.Where(p=>p.ModelID == car.ModelID).First();
                    marks.Add(UchPracticaEntities.GetContext().Mark.Where(p => p.MarkID == model.MarkID).First().Name);
                }
                return marks;
                
            }
        }

        public List<string> Model
        {
            get
            {
                List<string> models = new List<string>();

                foreach (OrderCar cars in orderCar)
                {
                    var car = UchPracticaEntities.GetContext().Car.Where(p => p.CarID == cars.CarID).First();
                    models.Add(UchPracticaEntities.GetContext().Model.Where(p => p.ModelID == car.ModelID).First().Name);
                    
                }
                return models;
            }
        }
    
    }
}
