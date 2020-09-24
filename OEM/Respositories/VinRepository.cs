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
            var mappedBasicVehicleInformation = new BasicVehicleInformation();
            var plantLocationCity = "";
            var plantLocationCountry = "";

            foreach (VehicleResult vehicleResult in result.Results)
            {
                if (vehicleResult.Variable == "Trim")
                {
                    mappedBasicVehicleInformation.Trim = vehicleResult.Value;
                }
                else if (vehicleResult.Variable == "Make")
                {
                    mappedBasicVehicleInformation.Make = vehicleResult.Value;
                }
                else if (vehicleResult.Variable == "Model")
                {
                    mappedBasicVehicleInformation.Model = vehicleResult.Value;
                }
                else if (vehicleResult.Variable == "Model Year")
                {
                    mappedBasicVehicleInformation.Year = vehicleResult.Value;
                }
                else if (vehicleResult.Variable == "Displacement (L)")
                {
                    mappedBasicVehicleInformation.Displacement = vehicleResult.Value;
                }
                else if (vehicleResult.Variable == "Engine Number of Cylinders")
                {
                    mappedBasicVehicleInformation.Cylinders = vehicleResult.Value;
                }
                else if (vehicleResult.Variable == "Plant City")
                {
                    plantLocationCity = vehicleResult.Value;
                }
                else if (vehicleResult.Variable == "Plant Country")
                {
                    plantLocationCountry = vehicleResult.Value;
                }

                if (vehicleResult.Variable == "Error Code" && vehicleResult.Value != "0")
                {
                    return (null, false);
                }
            }
            var truncatedVin = result.SearchCriteria.Substring(4, 17);

            mappedBasicVehicleInformation.Vin = truncatedVin;
            mappedBasicVehicleInformation.PlantLocation = plantLocationCity + ", " + plantLocationCountry;

            return (mappedBasicVehicleInformation, result.IsSuccess);
        }
    }
}
