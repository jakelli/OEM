using System;
using System.Threading.Tasks;
using OEM.Webservices.Dtos;

namespace OEM.Webservices
{
    public class MockVinWebservice : IVinWebservice
    {
        public MockVinWebservice()
        {
        }

        public Task<BasicVehicleInformationDto> GetBasicInformationByVin(string vin)
        {
            var response = new BasicVehicleInformationDto
            {
                IsSuccess = true
            };

            return Task.FromResult(response);
        }
    }
}
