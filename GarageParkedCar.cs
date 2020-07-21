using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeDebate.Samples.SpoAccessWebApi.Models
{
    public class GarageParkedCar
    {
        public string PlateNumber { get; set; }
        public string Driver { get; set; }
        public string ParkingSpot { get; set; }
        public string RecordCreated { get; set; }

        public GarageParkedCar(string plateNumber, string driver, string parkingSpot, string recordCreated)
        {
            PlateNumber = plateNumber;
            Driver = driver;
            ParkingSpot = parkingSpot;
            RecordCreated = recordCreated;
        }

        public GarageParkedCar()
        {
        }
    }
}