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

namespace UfimtsevUchPractica.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        List<FullCar> carsList = new List<FullCar>();
        User user = new User();
        public OrderPage(List<FullCar> cars,User currentUser)
        {
            InitializeComponent();

            DataContext = this;
            user = currentUser;
            carsList = cars;
            lViewOrder.ItemsSource= carsList;
            cmbPickUpPoint.ItemsSource = UchPracticaEntities.GetContext().PickUpPoint.ToList();
            txtUser.Text=user.UserSurname +" "+ user.UserName + " " + user.UserPatronymic;
        }
        private void UpdateData()
        {
            lViewOrder.ItemsSource = carsList;
            DataContext = this;
        }

        public string Total
        {
            get
            {
                var total = carsList.Sum(p => Convert.ToInt64(p.Price));
                return total.ToString();
            }
        }

        private void btnDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы уверены, что хотите удалить этот элемент?","Предупреждение", MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                carsList.Remove(lViewOrder.SelectedItem as FullCar);
            }
            UpdateData();
        }

        private void btnOrderSave_Click(object sender, RoutedEventArgs e)
        {
            var carsID = carsList.Select(p=> p.CarID).ToArray();
            if(cmbPickUpPoint.SelectedItem == null)
            {
                MessageBox.Show("Выберите пункт выдачи!");
                return;
            }
            if(carsList.Count == 0)
            {
                MessageBox.Show("Заказ пустой!");
                return;
            }
            try
            {
                Order order = new Order();

                order.OrderStatusID = 2;
                order.OrderDate = DateTime.Now;
                order.OrderDeliveryDate = order.OrderDate.AddDays(5);
                order.UserID = user.UserID;
                order.PickUpPointID = cmbPickUpPoint.SelectedIndex + 1;

                UchPracticaEntities.GetContext().Order.Add(order);

                var dbCars = UchPracticaEntities.GetContext().Car.ToList();

                for (int i = 0; i < carsID.Length; i++)
                {
                    OrderCar newOrderCar = new OrderCar()
                    {
                        OrderID = order.OrderID,
                        CarID = carsID[i],
                        CountCar = 1
                    };
                    var dbCar = dbCars.Where(c => c.CarID== carsID[i]).First();
                    dbCar.StatusID= 1;
                    UchPracticaEntities.GetContext().OrderCar.Add(newOrderCar);
                }
                
                UchPracticaEntities.GetContext().SaveChanges();
                MessageBox.Show("Заказ оформлен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }
    }
}
