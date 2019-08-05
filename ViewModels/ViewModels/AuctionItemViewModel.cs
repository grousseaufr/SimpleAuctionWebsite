using System;
using System.Collections.Generic;
using System.Linq;
using SimpleAuctionWebsite.Business.RestObjects;

namespace SimpleAuctionWebsite.Business.ViewModels
{
    public class AuctionItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal StartPrice { get; set; }
        public DateTime EndTime { get; set; }
        public bool DisplayBidForm { get; set; }
        public List<BidRequestDto> BidRequests { get; set; }

        public string LastBidAmount
        {
            get { return (BidRequests != null && BidRequests.Any()) ? $"${BidRequests.Max(m => m.Amount)}"  : string.Empty; }
        }

        public string FormattedEndTime => EndTime.ToString("f");
    }
}
