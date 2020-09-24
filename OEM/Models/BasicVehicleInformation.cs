using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OEM.Webservices.Dtos;
using Xamarin.Forms;

namespace OEM.Models
{
    public class BasicVehicleInformation
    {
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Vin { get; set; }
        public string Trim { get; set; }
        public string Cylinders { get; set; }
        public string Displacement { get; set; }
        public string PlantLocation { get; set; }
    }
}

