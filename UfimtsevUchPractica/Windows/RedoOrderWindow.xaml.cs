using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UfimtsevUchPractica.Entities;
using UfimtsevUchPractica.Pages;

namespace UfimtsevUchPractica.Windows
{
    /// <summary>
    /// Логика взаимодействия для RedoOrderWindow.xaml
    /// </summary>
    public partial class RedoOrderWindow : Window
    {
        List<CarsInOrder> Cars;
        List<CarsInOrder> deleted;


        public RedoOrderWindow(List<CarsInOrder> currentCars)
        {
            InitializeComponent();
            Cars = currentCars;
            lViewOrder.ItemsSource = currentCars;
            DataContext = this;

            CarsInOrder example = new CarsInOrder(
                "1", "1", "1", DateTime.Now, DateTime.Now, -1);

            List<CarsInOrder> parExample = new List<CarsInOrder>
            {
                example
            };
            deleted = parExample;
        }

        private void btnOrderDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }
        private void Delete()
        {
            try
            {
                if (deleted.Count == 1 && deleted[0].orderID == -1)
                {
                    deleted = Cars;
                }
                foreach (var toDelete in deleted)
                {
                    
                    var modelCar = UchPracticaEntities.GetContext().Model.Where(p => p.Name == toDelete.model).First();
                    var car = UchPracticaEntities.GetContext().Car.Where(p => p.ModelID == modelCar.ModelID).First();
                    var ordercar = UchPracticaEntities.GetContext().OrderCar.Where(p => p.OrderID == toDelete.orderID && p.CarID == car.CarID).First();
                    car.StatusID = 2;
                    UchPracticaEntities.GetContext().OrderCar.Remove(ordercar);
                }
                var a = deleted[0];
                var orderToDelete = UchPracticaEntities.GetContext().Order.Where(p => p.OrderID == a.orderID).First();
                UchPracticaEntities.GetContext().Order.Remove(orderToDelete);
                UchPracticaEntities._context.SaveChanges();
                MessageBox.Show("Заказ удален");
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
            
            return;
        }
        private void btnOrderRedo_Click(object sender, RoutedEventArgs e)
        {

            if (Cars.Count == 0)
            {
                Delete();
                return;
            }
            try
            {
                var a = Cars[0];
                var order = UchPracticaEntities.GetContext().Order.Where(p => p.OrderID == a.orderID).First();
                switch (cmbStatus.SelectedIndex)
                {
                    case -1:
                        MessageBox.Show("Выберите статус!");
                        return;
                    case 0:
                        order.OrderStatusID = 2;
                        break;
                    case 1:
                        order.OrderStatusID = 1;
                        break;
                }

                if (cmbUsers.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите клиента!");
                    return;
                }

                if (cmbPickUpPoint.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите пункт выдачи!");
                    return;
                }

                order.PickUpPointID = UchPracticaEntities.GetContext().PickUpPoint.Where(p => p.Address == cmbPickUpPoint.SelectedValue.ToString()).First().PickUpPointID;
                User user = new User();
                string[] fio = cmbUsers.SelectedItem.ToString().Split(' ');
                string g = fio[0];
                string b = fio[1];
                if (fio.Length == 3)
                {
                    string c = fio[2];
                    if (c == "")
                    {
                        c = null;
                    }
                    user = UchPracticaEntities.GetContext().User.Where(p => p.UserSurname == g && p.UserName == b && p.UserPatronymic == (c)).First();

                }
                else
                {
                    user = UchPracticaEntities.GetContext().User.Where(p => p.UserSurname == g && p.UserName == b).First();

                }
                order.UserID = user.UserID;
            }
            catch
            {
                MessageBox.Show("Заказ не найден");
                return;
            }
            if (deleted.Count > 1)
            {
                foreach (var toDelete in deleted)
                {
                    if (deleted.Count == 1 && toDelete.orderID == -1)
                    {

                        break;
                    }
                    var modelCar = UchPracticaEntities.GetContext().Model.Where(p => p.Name == toDelete.model).First();
                    var car = UchPracticaEntities.GetContext().Car.Where(p => p.ModelID == modelCar.ModelID).First();
                    var ordercar = UchPracticaEntities.GetContext().OrderCar.Where(p => p.OrderID == toDelete.orderID && p.CarID == car.CarID).First();
                    car.StatusID = 2;
                    UchPracticaEntities.GetContext().OrderCar.Remove(ordercar);

                }

            }
            UchPracticaEntities._context.SaveChanges();
            MessageBox.Show("Заказ отредактирован");
        }

        public string[] Statuses { get; set; } =
        {
            "Открыт",
            "Закрыт"
        };

        public string[] Users
        {
            get
            {
                List<string> result = new List<string>();
                var users = UchPracticaEntities.GetContext().User.Where(p => p.UserRoleID == 1).ToList();
                foreach (var user in users)
                {
                    string fio = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
                    result.Add(fio);
                }
                return result.ToArray();
            }
        }
        public string Total
        {
            get
            {
                return ((int)Cars.Sum(p => Convert.ToInt64(p.price))).ToString();

            }
        }

        public string[] Addresses
        {
            get
            {
                List<string> result = new List<string>();
                var addresses = UchPracticaEntities.GetContext().PickUpPoint.ToList();
                foreach (var address in addresses)
                {
                    result.Add(address.Address);
                }
                return result.ToArray();
            }
        }


        private void UpdateData()
        {
            lViewOrder.ItemsSource = Cars;
            DataContext = this;
        }

        private void btnDeleteSubOrder_Click(object sender, RoutedEventArgs e)
        {

           
            if (MessageBox.Show("Вы уверены, что хотите удалить этот элемент?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {             
                deleted.Add(lViewOrder.SelectedItem as CarsInOrder);
            }

        }

        private void lViewOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
