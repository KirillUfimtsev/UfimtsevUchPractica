using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UfimtsevUchPractica.Entities;

namespace UfimtsevUchPractica
{
    public class FullCar
    {


       public FullCar(int carID, int modelID, string vIN, int releaseYear, string pTS, string price, int statusID, string carImage, string mark, string model)
        {
            CarID = carID;
            ModelID = modelID;
            VIN = vIN;
            ReleaseYear = releaseYear;
            PTS = pTS;
            Price = price;
            StatusID = statusID;
            CarImage = carImage;
            Mark = mark; 
            Model = model;

        }
        public string imgPath
        {
            get
            {
                var path = "D:\\VSProjects\\UfimtsevUchPractica\\UfimtsevUchPractica\\Resources\\" + this.CarImage;
                return path;
            }
        }

        public string Status
        {
            get
            {
                if (StatusID == 1)
                {
                    return "Куплена";
                }

                else
                {
                    return "Продается";
                }
            }
        }

        public int CarID { get; }
        public int ModelID { get; }
        public string VIN { get; }
        public int ReleaseYear { get; }
        public string PTS { get; }
        public string Price { get; }
        public int StatusID { get; }
        public string CarImage { get; }
        public string Mark { get; }
        public string Model { get; }
    }
}
