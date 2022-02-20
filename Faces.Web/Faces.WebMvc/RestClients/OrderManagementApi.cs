using FaceWebMvc.ViewModels;

using MassTransit.ConsumeConfigurators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FaceWebMvc.RestClients
{
    public class OrderManagementApi : IOrderManagementApi
    {

        private readonly IOrderManagementApi _restClient;
        //private readonly IOptions<AppSettings> _settings;
        public OrderManagementApi(IConfiguration config, HttpClient httpClient)
        {
            //_settings = settings;
           // string apiHostAndPort = _settings.Value.OrdersApiUrl;
            string apiHostAndPort = config.GetSection("ApiServiceLocations").
                GetValue<string>("OrdersApiLocation");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api");
            _restClient = RestService.For<IOrderManagementApi>(httpClient);

        }
        public async Task<OrderViewModel> GetOrderById(Guid orderId)
        {
            try
            {
                return await _restClient.GetOrderById(orderId);
            }
            catch(ApiException ex)
            {
                if(ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<List<OrderViewModel>> GetOrders()
        {
            return await _restClient.GetOrders();
        }
    }
}
