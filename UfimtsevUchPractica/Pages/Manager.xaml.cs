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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UfimtsevUchPractica.Entities;
using UfimtsevUchPractica.Windows;

namespace UfimtsevUchPractica.Pages
{
    /// <summary>
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : Page
    {
        User user = new User();
        List<Order> Orders = new List<Order>(); 
        public Manager(User Currentuser)
        {
            InitializeComponent();
            var orders = UchPracticaEntities.GetContext().Order.ToList();
            LViewOrders.ItemsSource = orders;
            Orders = orders.ToList();
            user = Currentuser;
            DataContext = this;
            User();
            UpdateData();
        }

        Order selectedOrder;
        private void SelectOrder_Click(object sender, RoutedEventArgs e)
        {
            selectedOrder = LViewOrders.SelectedItem as Order;

            var orderCars = UchPracticaEntities.GetContext().OrderCar.Where(p=>p.OrderID == selectedOrder.OrderID).ToList();

            List<CarsInOrder> carsInOrder = new List<CarsInOrder>();
            foreach(var cars in orderCars)
            {
                Car car = UchPracticaEntities.GetContext().Car.Where(p=>p.CarID == cars.CarID).First();
                var model = UchPracticaEntities.GetContext().Model.Where(p => p.ModelID == car.ModelID).First();
                var mark = UchPracticaEntities.GetContext().Mark.Where(p => p.MarkID == model.MarkID).First();
                CarsInOrder carsIn = new CarsInOrder
                    (
                        mark.Name,
                        model.Name,
                        car.Price,
                        selectedOrder.OrderDate,
                        selectedOrder.OrderDeliveryDate,
                        selectedOrder.OrderID
                    );
                carsInOrder.Add(carsIn);
            }

            RedoOrderWindow redoOrderWindow = new RedoOrderWindow(carsInOrder);
            redoOrderWindow.ShowDialog();
        }

        private void User()
        {
            if (user != null)
            {
                txtFullname.Text = user.UserSurname.ToString() + " " + user.UserName.ToString() + " " + user.UserPatronymic?.ToString();
            }

            else
            {

            }
        }

        public string[] SortingList { get; set; } =
        {
            "Без сортировки",
            "Стоимость по возрастанию",
            "Стоимость по убыванию",
            "Сначала открытые",
            "Сначала закрытые"
        };

        private void UpdateData()
        {
            var result = UchPracticaEntities.GetContext().Order.ToList();

            switch (cmbSorting.SelectedIndex)
            {
                case 1:
                    result = result.OrderBy(p => Convert.ToInt64(p.Cost)).ToList();
                    break;
                case 2:
                    result = result.OrderByDescending(p => Convert.ToInt64(p.Cost)).ToList();
                    break;
                case 3:
                    result = result.OrderBy(p => p.Status == "Закрыт").ToList();
                    break;
                case 4:
                    result = result.OrderBy(p => p.Status == "Открыт").ToList();
                    break;
            }
            LViewOrders.ItemsSource = result;

        }



        private void LViewOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        
    }
}
