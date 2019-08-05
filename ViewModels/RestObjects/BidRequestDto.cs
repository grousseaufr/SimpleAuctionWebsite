using System;

namespace SimpleAuctionWebsite.Business.RestObjects
{
    public class BidRequestDto
    {
        public DateTime BidTime { get; set; }
        public int AuctionItemId { get; set; }
        public decimal Amount { get; set; }
    }
}