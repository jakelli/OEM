using System;
using System.Threading.Tasks;
using OEM.Webservices.Dtos;

namespace OEM.Webservices
{
    public interface IVinWebservice
    {
        Task<BasicVehicleInformationDto> GetBasicInformationByVin(string vin);
    }
}
