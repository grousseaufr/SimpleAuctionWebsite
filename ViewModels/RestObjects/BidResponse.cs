namespace SimpleAuctionWebsite.Business.RestObjects
{
    public class BidResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int CurrentBidCount { get; set; }
        public BidRequestDto BidRequestDto { get; set; }
    }
}