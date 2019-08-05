using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAuctionWebsite.Business.Builders;
using SimpleAuctionWebsite.Business.Helpers;
using SimpleAuctionWebsite.Business.RestObjects;
using SimpleAuctionWebsite.Business.ViewModels;
using SimpleAuctionWebsite.Models;

namespace SimpleAuctionWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuctionItemViewModelBuilder _auctionItemViewModelBuilder;
        private readonly BidHelper _bidHelper;

        public HomeController(AuctionItemViewModelBuilder auctionItemViewModelBuilder, BidHelper bidHelper)
        {
            this._auctionItemViewModelBuilder = auctionItemViewModelBuilder;
            _bidHelper = bidHelper;
        }

        public async Task<IActionResult> Index()
        {
            var indexViewModel = new IndexViewModel
            {
                AuctionItemViewModels = await _auctionItemViewModelBuilder.GetItems()
            };
            return View(indexViewModel);
        }

        public async Task<IActionResult> AuctionItemDetails(int id)
        {
            var auctionItemViewModel = await _auctionItemViewModelBuilder.GetItem(id);
            //auctionItemViewModel.DisplayBidForm = true;
            return View(auctionItemViewModel);
        }

        public async Task<IActionResult> PlaceBid(BidViewModel bidViewModel)
        {
            bidViewModel.BidTime = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false,
                                            message = ModelState.SelectMany(m => m.Value.Errors)
                                                .Select(m => m.ErrorMessage)
                                                .ToList()});

            }

            BidResponse bidResponse = await _bidHelper.PlaceBid(bidViewModel);

            return new JsonResult(new { success = bidResponse.Success, message = bidResponse.Message, bidCount = bidResponse.CurrentBidCount});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
