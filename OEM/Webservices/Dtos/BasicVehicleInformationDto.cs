using System;
using System.Collections.Generic;

namespace OEM.Webservices.Dtos
{
    public class BasicVehicleInformationDto : Status
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public string SearchCriteria { get; set; }
        public List<VehicleResult> Results { get; set; }
    }

    
}
