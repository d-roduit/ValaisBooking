using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using ValaisBooking.Models;

namespace ValaisBooking.Controllers
{
    /// <summary>
    ///     The HomeContoller class manages URLs relating to the home page <c>/[...]</c> or <c>/Home/[...]</c>
    /// </summary>
    public class HomeController : Controller
    {
        /// <value>The default logger object provided by Microsoft</value>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        ///     Constructor of the HomeController class
        /// </summary>
        /// <param name="logger">
        ///     A logger object for HomeController
        /// </param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Manage calls to the URLs <c>/</c> or <c>/Home/</c> with a GET request
        /// </summary>
        /// <returns>
        ///     The Index view
        /// </returns>
        public IActionResult Index()
        {
            SearchHotelsViewModel searchHotelsViewModel = new SearchHotelsViewModel();

            if (HttpContext.Session.GetString("destination") != null)
            {
                searchHotelsViewModel.Destination = HttpContext.Session.GetString("destination");
            }

            if (HttpContext.Session.GetString("checkIn") != null)
            {
                searchHotelsViewModel.CheckIn = Convert.ToDateTime(HttpContext.Session.GetString("checkIn"));
            }

            if (HttpContext.Session.GetString("checkOut") != null)
            {
                searchHotelsViewModel.CheckOut = Convert.ToDateTime(HttpContext.Session.GetString("checkOut"));
            }

            if (!string.IsNullOrEmpty(searchHotelsViewModel.Destination) && searchHotelsViewModel.CheckIn != default && searchHotelsViewModel.CheckOut != default)
            {
                return View(searchHotelsViewModel);
            }

            return View();
        }

        /// <summary>
        ///     Manage calls to the URLs <c>/</c> or <c>/Home/</c> with a POST request
        /// </summary>
        /// <param name="searchHotelsViewModel">
        ///     The ViewModel that store search information to find hotels
        /// </param>
        /// <returns>
        ///     The Index view of Home
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SearchHotelsViewModel searchHotelsViewModel)
        {
            string destination = searchHotelsViewModel.Destination;
            DateTime checkIn = searchHotelsViewModel.CheckIn;
            DateTime checkOut = searchHotelsViewModel.CheckOut;


            if (DateTime.Equals(checkIn, default(DateTime)))
            {
                ModelState.ClearValidationState("CheckIn");
                ModelState.AddModelError("CheckIn", "Check In is required");
            }
            else if (checkOut.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("CheckIn", "Check In cannot be before today");
            }

            if (DateTime.Equals(checkOut, default(DateTime)))
            {
                ModelState.ClearValidationState("CheckOut");
                ModelState.AddModelError("CheckOut", "Check Out is required");
            }
            else if (checkOut.Date < DateTime.Now.AddDays(1).Date)
            {
                ModelState.AddModelError("CheckOut", "Check Out cannot be before tomorrow");
            }

            if (checkOut.Date < checkIn.Date)
            {
                ModelState.AddModelError("CheckOut", "Check out date cannot be before check in date");
            }
            
            if (!ModelState.IsValid)
            {
                return View(searchHotelsViewModel);
            }

            HttpContext.Session.SetString("destination", destination);
            HttpContext.Session.SetString("checkIn", checkIn.ToString());
            HttpContext.Session.SetString("checkOut", checkOut.ToString());

            return RedirectToAction("GetHotels", "Hotel", searchHotelsViewModel);
        }
    }
}
