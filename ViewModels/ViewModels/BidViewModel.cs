using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleAuctionWebsite.Business.ViewModels
{
    public class BidViewModel
    {
        public int AuctionItemId { get; set; }
        [Required(ErrorMessage = "Bid Amount is required")]
        public decimal? Amount { get; set;}
        public DateTime BidTime { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

    }
}