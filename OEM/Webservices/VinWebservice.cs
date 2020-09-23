using System;
using System.Net.Http;
using System.Threading.Tasks;
using OEM.Webservices.Contracts;
using OEM.Webservices.Dtos;
using Refit;

namespace OEM.Webservices
{
    public class VinWebservice : IVinWebservice
    {
        private readonly IVinController _vinController;

        private RequestProvider _requestProvider;

        public VinWebservice()
        {
            _vinController = RestService.For<IVinController>(GetHttpClient());
        }

        #region Basic Vehicle Information
        public async Task<BasicVehicleInformationDto> GetBasicInformationByVin(string vin) {
            var request = _vinController.GetBasicVehicleInformation(GetAuth(), vin);
            return await _requestProvider.MakeRequestAsync(request, _requestProvider.GetDefaultPolicyWithRetries(2));
        }
        #endregion

        private HttpClient GetHttpClient()
        {

            var handler = new HttpClientHandler();
            return new HttpClient(handler) { BaseAddress = new Uri($"https://test") };
        }

        private string GetAuth()
        {
            return "Basic test";
        }

    }
}
