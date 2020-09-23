using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using OEM.Models;
using OEM.Webservices;
using OEM.Webservices.Dtos;

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

            ObservableCollection<VehicleResult> results = new ObservableCollection<VehicleResult>();

            foreach (VehicleResult vehicleResult in result.Results)
            {
                if (vehicleResult.Variable == "Body Class"
                    || vehicleResult.Variable == "Engine Brake (hp)"
                    || vehicleResult.Variable == "Make"
                    || vehicleResult.Variable == "Model"
                    || vehicleResult.Variable == "Model Year")
                {
                    results.Add(vehicleResult);
                }
            }

            var mappedResult = new BasicVehicleInformation
            {
                Count = result.Count,
                SearchCriteria = result.SearchCriteria,
                Message = result.Message,
                Results = results
            };
            return (mappedResult, result.IsSuccess);
        }
    }
}
