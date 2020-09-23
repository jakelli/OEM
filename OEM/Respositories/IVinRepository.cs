using System;
using System.Threading.Tasks;
using OEM.Models;
using Xamarin.Forms;

namespace OEM.Respositories
{
    public interface IVinRepository
    {
        Task<(BasicVehicleInformation, bool IsSuccess)> GetBasicInformationByVin(string vin);
    }
}

