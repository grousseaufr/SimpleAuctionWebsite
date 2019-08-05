using System;
using System.Collections.Generic;

namespace SimpleAuctionWebsite.Business.RestObjects
{
    public class AuctionItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal StartPrice { get; set; }
        public DateTime EndTime { get; set; }
        public List<BidRequestDto> BidRequests { get; set; }
    }
}