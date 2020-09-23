using System;
using System.Threading.Tasks;
using OEM.Models;
using OEM.Webservices;

namespace OEM.Respositories
{
    public class VinRepository : IVinRepository
    {
        private IVinWebservice _vinWebservice;

        public VinRepository(IVinWebservice vinWebservice)
        {
            _vinWebservice = vinWebservice;
        }

        public async Task<(BasicVehicleInformation BasicVehicleInformation, bool IsSuccess)> GetBasicInformationByVin(string vin)
        {
            var result = await _vinWebservice.GetBasicInformationByVin(vin);
            var mappedResult = new BasicVehicleInformation
            {
                Make = result.Make,
                Year = result.Year,
                Model = result.Model
            };

            return (mappedResult, result.IsSuccess);
        }
    }
}
