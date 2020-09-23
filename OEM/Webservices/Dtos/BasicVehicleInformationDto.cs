using System;
namespace OEM.Webservices.Dtos
{
    public class BasicVehicleInformationDto : Status
    {
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }
}
