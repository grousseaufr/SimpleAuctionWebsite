using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SimpleAuctionWebsite.Business.RestObjects;
using SimpleAuctionWebsite.Business.ViewModels;

namespace SimpleAuctionWebsite.Business.Builders
{
    public class AuctionItemViewModelBuilder
    {
        private static HttpClient _httpClient = null;
    
        public AuctionItemViewModelBuilder()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient {BaseAddress = new Uri("http://localhost:53712/")};
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public async Task<List<AuctionItemViewModel>> GetItems()
        {
            List<AuctionItemDto> auctionItemDtos = null;

            HttpResponseMessage response =  await _httpClient.GetAsync("api/auction");
            if (response.IsSuccessStatusCode)
            {
                auctionItemDtos = await response.Content.ReadAsAsync<List<AuctionItemDto>>();
            }

            List<AuctionItemViewModel> auctionItemViewModel = Adapt(auctionItemDtos);

            return auctionItemViewModel;
        }

        public async Task<AuctionItemViewModel> GetItem(int id)
        {
            AuctionItemDto auctionItemDto = null;

            HttpResponseMessage response = await _httpClient.GetAsync($"api/auction/{id}");
            if (response.IsSuccessStatusCode)
            {
                auctionItemDto = await response.Content.ReadAsAsync<AuctionItemDto>();
            }

            AuctionItemViewModel auctionItemViewModel = Adapt(auctionItemDto);

            return auctionItemViewModel;
        }

        private List<AuctionItemViewModel> Adapt(List<AuctionItemDto> itemDto)
        {
            return itemDto != null ? itemDto.Select(Adapt).ToList() : new List<AuctionItemViewModel>();
        }

        private AuctionItemViewModel Adapt(AuctionItemDto itemDto)
        {
            if (itemDto != null)
            {
                return new AuctionItemViewModel
                {
                    Id = itemDto.Id,
                    Name = itemDto.Name,
                    Description = itemDto.Description,
                    ImageUrl = itemDto.ImageUrl,
                    StartPrice = itemDto.StartPrice,
                    EndTime = itemDto.EndTime,
                    BidRequests = itemDto.BidRequests
                };
            }

            return null;
        }
    }
}
