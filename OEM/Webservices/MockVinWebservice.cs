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
                ErrorCode = "",
                ErrorMessage = "",
                IsSuccess = true,
                Year = "2015",
                Make = "BMW",
                Model = "335i"
            };

            return Task.FromResult(response);
        }
    }
}
