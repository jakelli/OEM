using System;
using System.Threading.Tasks;
using OEM.Webservices.Dtos;
using Refit;

namespace OEM.Webservices.Contracts
{
    public interface IVinController
    {
        [Headers("Accept: application/json")]
        [Get("/test/url")]
        Task<BasicVehicleInformationDto> GetBasicVehicleInformation(
            [Header("Authorization")] string auth,
            string vin);
    }
}
