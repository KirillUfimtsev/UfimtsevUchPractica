using Microsoft.Win32;
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

namespace UfimtsevUchPractica.Windows
{
    /// <summary>
    /// Логика взаимодействия для RedoCarWindow.xaml
    /// </summary>
    public partial class RedoCarWindow : Window
    {
        Car car = new Car();
        public RedoCarWindow(Car currentcar)
        {
            InitializeComponent();
            car = currentcar;
            DataContext = this;

            txtPrice.Text = car.Price;
            txtPTS.Text = car.PTS;  
            txtVIN.Text = car.VIN;  
            txtYear.Text = car.ReleaseYear.ToString();
            string uri = "D:/VSProjects/UfimtsevUchPractica/UfimtsevUchPractica/Resources/" + car.CarImage;
            img.Source = new BitmapImage(new Uri(uri));
        }

        public string ReleaseYear
        {
            get
            {
                return UchPracticaEntities.GetContext().Car.Where(p => p.CarID == car.CarID).First().ReleaseYear.ToString();
            }
        }

        public string VIN
        {
            get
            {
                return UchPracticaEntities.GetContext().Car.Where(p => p.CarID == car.CarID).First().VIN.ToString();
            }
        }

        public string PTS
        {
            get
            {
                return UchPracticaEntities.GetContext().Car.Where(p => p.CarID == car.CarID).First().PTS.ToString();
            }
        }

        public string Price
        {
            get
            {
                return UchPracticaEntities.GetContext().Car.Where(p => p.CarID == car.CarID).First().Price.ToString();
            }
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckVIN(txtVIN.Text))
            {
                MessageBox.Show("Неверный ввод VIN");
                return;
            }

            if (!CheckPTS(txtPTS.Text))
            {
                MessageBox.Show("Неверный ввод PTS");
                return;
            }

            if (!CheckPrice(txtPrice.Text))
            {
                MessageBox.Show("Неверный ввод цены");
                return;
            }

            if (!CheckYear(txtYear.Text))
            {
                MessageBox.Show("Неверный ввод года выпуска");
                return;
            }
            var Cmbmodel = cmbModel.SelectedValue.ToString();

            
            car.StatusID = 2;
            car.ModelID = UchPracticaEntities.GetContext().Model.Where(p => p.Name == Cmbmodel).First().ModelID;
            car.VIN = txtVIN.Text;
            car.PTS = txtPTS.Text;
            car.Price = txtPrice.Text;
            car.ReleaseYear = (int)Convert.ToInt64(txtYear.Text);

            if (img.Source.ToString() == "D:\\VSProjects\\UfimtsevUchPractica\\UfimtsevUchPractica\\Resources\\picture.png")
            {
                car.CarImage = null;
            }
            else
            {
                List<string> path = img.Source.ToString().Split('/').ToList();
                car.CarImage = path.Last();
            }
            UchPracticaEntities._context.SaveChanges();
            MessageBox.Show("Изменения сохранены");

        }



        public string[] ok_symbols = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };


        private Boolean CheckYear(string year)
        {
            var check = int.TryParse(year, out var da);
            return check;
        }

        private Boolean CheckPrice(string price)
        {
            var check = int.TryParse(price, out var da);
            return check;
        }
        private Boolean CheckVIN(string vin)
        {
            // Проверка на длину строки
            if (vin.Length != 17) { return false; }

            // Проверка на наличие "Неправильных символов"
            for (int i = 0; i < vin.Length; i++)
            {
                if (!ok_symbols.Contains(vin.Substring(i, 1))) { return false; }
            }
            return true;
        }

        private Boolean CheckPTS(string pts)
        {
            if (pts.Length != 10)
            {
                return false;
            }
            bool parses = int.TryParse(pts.Substring(0, 2), out var da);
            bool parses1 = int.TryParse(pts.Substring(4, 6), out var dada);

            if (!(parses && parses1))
            {
                return false;
            }
            return true;
        }


        public string[] Mark
        {
            get
            {
                List<string> names = new List<string>();
                var marks = UchPracticaEntities.GetContext().Mark.ToList();
                foreach (var mark in marks)
                {
                    names.Add(mark.Name);
                }
                return names.ToArray();
            }
        }




        private void cmbMark_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> result = new List<string>();
            var Cmbmark = cmbMark.SelectedValue.ToString();
            if (Cmbmark == null)
            {
                return;
            }

            var mark = UchPracticaEntities.GetContext().Mark.Where(p => p.Name == Cmbmark).First();
            var models = UchPracticaEntities.GetContext().Model.Where(p => p.MarkID == mark.MarkID).ToList();
            foreach (var model in models)
            {
                result.Add(model.Name);
            }
            cmbModel.ItemsSource = result;
        }

        private void btnEnterImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Файлы изображений: (*.png, *.jpg, *.jpeg)| *.png; *.jpg; *.jpeg";
            fileDialog.InitialDirectory = "D:\\VSProjects\\UfimtsevUchPractica\\UfimtsevUchPractica\\Resources\\";
            if (fileDialog.ShowDialog() == true)
            {
                string uri = "D:\\VSProjects\\UfimtsevUchPractica\\UfimtsevUchPractica\\Resources\\" + fileDialog.SafeFileName;
                img.Source = new BitmapImage(new Uri(uri));
            }
        }
    }
}
