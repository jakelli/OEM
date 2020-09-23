using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OEM.Webservices.Dtos;
using Xamarin.Forms;

namespace OEM.Models
{
    public class BasicVehicleInformation
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public string SearchCriteria { get; set; }
        public ObservableCollection<VehicleResult> Results { get; set; }
    }
}

