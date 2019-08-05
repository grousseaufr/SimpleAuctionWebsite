using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleAuctionWebsite.Business.RestObjects;
using SimpleAuctionWebsite.Business.ViewModels;

namespace SimpleAuctionWebsite.Business.Helpers
{
    public class BidHelper
    {
        private static HttpClient _httpClient = null;

        public BidHelper()
        {
            if (_httpClient == null)
            {

                _httpClient = new HttpClient {BaseAddress = new Uri("http://localhost:53712/")};
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }
        public async Task<BidResponse> PlaceBid(BidViewModel bidViewModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(bidViewModel), Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("api/bid", stringContent);
            
            // Authentication header is never sent in request, don't understand why ... 
            //var byteArray = Encoding.ASCII.GetBytes($"{bidViewModel.UserName}:{bidViewModel.Password}");
            //var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "api/bid");
            ////httpRequestMessage.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{bidViewModel.UserName}:{bidViewModel.Password}")));
            //httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", $"{bidViewModel.UserName}:{bidViewModel.Password}");
            //httpRequestMessage.Content = stringContent;
            //var response = await _httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var bidResponse = await response.Content.ReadAsAsync<BidResponse>();
                return bidResponse;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new BidResponse
                {
                    Success = false,
                    Message = "You are not logged in."
                };
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new BidResponse
                {
                    Success = false,
                    Message = $"Bad request : {message}"
                };
            }

            return new BidResponse
            {
                Success = false,
                Message = $"Error {response.StatusCode} : {response.ReasonPhrase}"
            };
        }
    }
}