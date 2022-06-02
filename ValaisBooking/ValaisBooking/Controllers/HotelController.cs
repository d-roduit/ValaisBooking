using BusinessLogicLayer;
using DataTransferObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ValaisBooking.Models;

namespace ValaisBooking.Controllers
{
    /// <summary>
    ///     The HotelContoller class manages URLs relating to the hotels <c>/Hotel/[...]</c>
    /// </summary>
    public class HotelController : Controller
    {
        /// <value>The HotelManager object from the Business Logic Layer that retrieves all information relating to the hotels</value>
        private IHotelManager HotelManager { get; }

        /// <value>The RoomManager object from the Business Logic Layer that retrieves all information relating to the hotel rooms</value>
        private IRoomManager RoomManager { get; }

        /// <value>The PictureManager object from the Business Logic Layer that retrieves all information relating to the room prictures</value>
        private IPictureManager PictureManager { get; }

        /// <summary>
        ///     Constructor of the HotelController class
        /// </summary>
        /// <param name="hotelManager">
        ///     The HotelManager object from the Business Logic Layer that retrieves all information relating to the hotels
        /// </param>
        /// <param name="roomManager">
        ///     The RoomManager object from the Business Logic Layer that retrieves all information relating to the rooms
        /// </param>
        /// <param name="pictureManager">
        ///     The PictureManager object from the Business Logic Layer that retrieves all information relating to the room prictures
        /// </param>
        public HotelController(IHotelManager hotelManager, IRoomManager roomManager, IPictureManager pictureManager)
        {
            HotelManager = hotelManager;
            RoomManager = roomManager;
            PictureManager = pictureManager;
        }

        /// <summary>
        ///     Manage calls to the URLs <c>/Hotel/</c> or <c>/Hotel/Index</c> with a GET request
        /// </summary>
        /// <returns>
        ///     The Index view of Hotel
        /// </returns>
        public ActionResult Index()
        {
            List<Hotel> hotels = HotelManager.GetAllHotels();
            return View(hotels);
        }

        /// <summary>
        ///     Manage calls to the URL <c>/Hotel/GetHotels/[...]</c> with a GET request
        /// </summary>
        /// <param name="searchHotelsViewModel">
        ///     The ViewModel that store search information to find hotels
        /// </param>
        /// <returns>
        ///     The GetHotels view of Hotel for the given search information stored inside the <c>SearchHotelsViewModel</c> object
        /// </returns>
        [HttpGet]
        public IActionResult GetHotels(SearchHotelsViewModel searchHotelsViewModel)
        {
            DateTime checkIn = searchHotelsViewModel.CheckIn;
            DateTime checkOut = searchHotelsViewModel.CheckOut;


            if (DateTime.Equals(checkIn, default(DateTime)))
            {
                ModelState.ClearValidationState("searchHotelsViewModel.CheckIn");
                ModelState.AddModelError("searchHotelsViewModel.CheckIn", "Check In is required");
            }
            else if (checkIn.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("searchHotelsViewModel.CheckIn", "Check In cannot be before today");
            }

            if (DateTime.Equals(checkOut, default(DateTime)))
            {
                ModelState.ClearValidationState("searchHotelsViewModel.CheckOut");
                ModelState.AddModelError("searchHotelsViewModel.CheckOut", "Check Out is required");
            }
            else if (checkOut.Date < DateTime.Now.AddDays(1).Date)
            {
                ModelState.AddModelError("searchHotelsViewModel.CheckOut", "Check Out cannot be before tomorrow");
            }

            if (checkOut.Date < checkIn.Date)
            {
                ModelState.AddModelError("searchHotelsViewModel.CheckOut", "Check out date cannot be before check in date");
            }

            if (!ModelState.IsValid)
            {
                GetHotelsViewModel invalidStateGetHotelsViewModel = new GetHotelsViewModel(searchHotelsViewModel, null);

                return View(invalidStateGetHotelsViewModel);
            }

            ViewData["showBookButton"] = true;

            string destination = searchHotelsViewModel.Destination;
            bool hasWifi = searchHotelsViewModel.HasWifi;
            bool hasParking = searchHotelsViewModel.HasParking;
            List<bool> categories = searchHotelsViewModel.Stars;
            bool hasTV = searchHotelsViewModel.HasTV;
            bool hasHairDryer = searchHotelsViewModel.HasHairDryer;
            int minPrice = 0;
            int maxPrice = searchHotelsViewModel.MaxPrice;
            bool simpleRoom = searchHotelsViewModel.SimpleRoom;
            bool doubleRoom = searchHotelsViewModel.DoubleRoom;

            List<Hotel> hotels = HotelManager.GetHotels(destination, checkIn, checkOut, hasWifi, hasParking, categories, hasTV, hasHairDryer, minPrice, maxPrice, simpleRoom, doubleRoom);

            if (hotels != null)
            {
                HttpContext.Session.SetString("destination", destination);
                HttpContext.Session.SetString("checkIn", checkIn.ToString());
                HttpContext.Session.SetString("checkOut", checkOut.ToString());
                HttpContext.Session.SetInt32("hasTV", hasTV ? 1 : 0);
                HttpContext.Session.SetInt32("hasHairDryer", hasHairDryer ? 1 : 0);
                HttpContext.Session.SetInt32("maxPrice", maxPrice);
                HttpContext.Session.SetInt32("simpleRoom", simpleRoom ? 1 : 0);
                HttpContext.Session.SetInt32("doubleRoom", doubleRoom ? 1 : 0);
            }

            GetHotelsViewModel getHotelsViewModel = new GetHotelsViewModel(searchHotelsViewModel, hotels);

            return View(getHotelsViewModel);
        }

        /// <summary>
        ///     This method shows the Details view of an hotel for the given hotel id
        ///     Manage calls to the URL <c>/Hotel/Details/[...]</c> with a GET request
        /// </summary>
        /// <param name="id">
        ///     The hotel id
        /// </param>
        /// <param name="hasTV">
        ///     A boolean value to say if the rooms must have a TV
        /// </param>
        /// <param name="hasHairDryer">
        ///     A boolean value to say if the rooms must have a hair dryer
        /// </param>
        /// <param name="maxPrice">
        ///     The maximum price of the rooms
        /// </param>
        /// <param name="simpleRoom">
        ///     A boolean value to say if the rooms must be simple rooms
        /// </param>
        /// <param name="doubleRoom">
        ///     A boolean value to say if the rooms must be double rooms
        /// </param>
        /// <param name="checkIn">
        ///     The date from which the rooms must be free
        /// </param>
        /// <param name="checkOut">
        ///     The date until which the rooms must be free
        /// </param>
        /// <returns>
        ///     The Details view of Hotel for the given search information and filters
        /// </returns>
        public ActionResult Details(int id, bool hasTV, bool hasHairDryer, int maxPrice, bool simpleRoom, bool doubleRoom, DateTime checkIn, DateTime checkOut)
        {
            Hotel hotel = HotelManager.GetHotel(id);

            if (hotel == null)
            {
                return View("Error", new ErrorViewModel { RequestId = id.ToString() });
            }

            List<Room> rooms = null;
            List<List<Picture>> picturesByRoom = null;

            if (checkIn != default(DateTime) && checkIn != null && checkOut != default(DateTime) && checkOut != null)
            {
                int minPrice = 0;

                rooms = RoomManager.GetAvailableRoomsByHotel(id, checkIn, checkOut, hasTV, hasHairDryer, minPrice, maxPrice, simpleRoom, doubleRoom);
                ViewData["checkInDate"] = checkIn.ToString("dd.MM.yyyy");
                ViewData["checkOutDate"] = checkOut.ToString("dd.MM.yyyy");

                bool mustIncreasePrice = HotelManager.IsOccupancyOver(70, id, checkIn, checkOut);

                if(rooms != null)
                {
                    if (mustIncreasePrice)
                    {
                        foreach (Room room in rooms)
                        {
                            room.Price *= (decimal)1.2;
                        }
                    }
                } 
            }
            else
            {
                rooms = RoomManager.GetRoomsByHotel(id);
            }

            if (rooms != null)
            {
                picturesByRoom = new List<List<Picture>>();

                foreach (Room room in rooms)
                {
                    List<Picture> pictures = PictureManager.GetPicturesByRoom(room.IdRoom);

                    picturesByRoom.Add(pictures);
                }
            }

            HotelDetailsViewModel hotelRoomsViewModel = new HotelDetailsViewModel(hotel, rooms, picturesByRoom);

            return View(hotelRoomsViewModel);
        }
    }
}