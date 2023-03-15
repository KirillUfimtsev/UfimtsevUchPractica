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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        User user = new User();
        public Admin(User currentuser)
        {
            InitializeComponent();
            user = currentuser;
            var cars = UchPracticaEntities.GetContext().Car.ToList();
            LViewProduct.ItemsSource = GetData(cars);
            DataContext = this;
            UpdateData();
            User();
        }

        private List<FullCar> GetData(List<Car> cars)
        {
            List<FullCar> fullCar = new List<FullCar>();
            var model = UchPracticaEntities.GetContext().Model.ToList();
            var mark = UchPracticaEntities.GetContext().Mark.ToList();
            foreach (Car car in cars)
            {
                var ModelName = model.Where(p => p.ModelID == car.ModelID).First();
                var MarkName = mark.Where(p => p.MarkID == ModelName.MarkID).First().Name;
                var newCar = new FullCar(car.CarID, car.ModelID, car.VIN, car.ReleaseYear, car.PTS, car.Price, car.StatusID, car.CarImage, MarkName, ModelName.Name);
                fullCar.Add(newCar);
            }

            return fullCar;

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

        private void UpdateData()
        {
            var result = UchPracticaEntities.GetContext().Car.ToList();

            switch (cmbSorting.SelectedIndex)
            {
                case 1:
                    result = result.OrderBy(p => Convert.ToInt64(p.Price)).ToList();
                    break;
                case 2:
                    result = result.OrderByDescending(p => Convert.ToInt64(p.Price)).ToList();
                    break;
                case 3:
                    result = result.OrderBy(p => p.StatusID).ToList();
                    break;
                case 4:
                    result = result.OrderByDescending(p => p.StatusID).ToList();
                    break;
            }
            LViewProduct.ItemsSource = GetData(result);

        }
        public string[] SortingList { get; set; } =
        {
            "Без сортировки",
            "Стоимость по возрастанию",
            "Стоимость по убыванию",
            "Куплены",
            "Продаются"
        };

        private void cmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        

        private void btnAddCar_Click(object sender, RoutedEventArgs e)
        {
            AddCarWindow addCarWindow = new AddCarWindow();
            addCarWindow.ShowDialog();
        }

        private void btnRedoCar_Click(object sender, RoutedEventArgs e)
        {
            FullCar fcar = LViewProduct.SelectedItem as FullCar;
            var car = UchPracticaEntities.GetContext().Car.Where(p => p.CarID == fcar.CarID).First();

            try
            {
                var orderCar = UchPracticaEntities.GetContext().OrderCar.Where(p => p.CarID == car.CarID).First();
                if (orderCar != null)
                {
                    MessageBox.Show("Данный автомобиль находится в заказе, его редактировать невозможно!");
                    return;
                }
            }
            catch
            {

            }
            RedoCarWindow redoCarWindow = new RedoCarWindow(car);
            redoCarWindow.ShowDialog();
        }

        private void btnDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот элемент?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                FullCar fcar = LViewProduct.SelectedItem as FullCar;
                var car = UchPracticaEntities.GetContext().Car.Where(p => p.CarID == fcar.CarID).First();

                try
                {
                    var orderCar = UchPracticaEntities.GetContext().OrderCar.Where(p => p.CarID == car.CarID).First();
                    if(orderCar != null)
                    {
                        MessageBox.Show("Данный автомобиль находится в заказе, его удалить невозможно!");
                        return;
                    }
                }
                catch
                {
                    
                }
                UchPracticaEntities._context.Car.Remove(car);
                UchPracticaEntities._context.SaveChanges();
            }
            else
            {
                return;
            }
            UpdateData();
        }

        private void LViewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
