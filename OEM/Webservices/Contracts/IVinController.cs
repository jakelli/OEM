using System;
using System.Threading.Tasks;
using OEM.Webservices.Dtos;
using Refit;

namespace OEM.Webservices.Contracts
{
    public interface IVinController
    {
        [Get("/vehicles/DecodeVin/{vin}?format=json")]
        Task<BasicVehicleInformationDto> GetBasicVehicleInformation(string vin);
    }
}
