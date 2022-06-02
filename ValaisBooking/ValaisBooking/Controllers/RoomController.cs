using BusinessLogicLayer;
using DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ValaisBooking.Models;

namespace ValaisBooking.Controllers
{
    /// <summary>
    /// The RoomController class manages URLs relating to the room <c>/Room/[...]</c>
    /// </summary>
    public class RoomController : Controller
    {
        /// <value>The RoomManager object from the Business Logic Layer that retrieves all information relating to the hotel rooms</value>
        private IRoomManager RoomManager { get; }

        /// <value>The PictureManager object from the Business Logic Layer that retrieves all information relating to the room prictures</value>
        private IPictureManager PictureManager { get; }

        /// <summary>
        ///     Constructor of the RoomController class
        /// </summary>
        /// <param name="roomManager">
        ///     The RoomManager object from the Business Logic Layer that retrieves all information relating to the rooms
        /// </param>
        /// <param name="pictureManager">
        ///     The PictureManager object from the Business Logic Layer that retrieves all information relating to the room prictures
        /// </param>
        public RoomController(IRoomManager roomManager, IPictureManager pictureManager)
        {
            RoomManager = roomManager;
            PictureManager = pictureManager;
        }

        /// <summary>
        ///     This method shows the Details View of a room for the given room id
        ///     It manages calls to the URL <c>/Room/Details/[...]</c> with a GET request
        /// </summary>
        /// <param name="id">
        ///     The room id
        /// </param>
        /// <returns>
        ///     The Details View fr the given roomDetails ViewModel
        /// </returns>
        [HttpGet]
        public IActionResult Details(int id)
        {
            Room room = RoomManager.GetRoom(id);

            List<Picture> pictures = null;

            if(room != null)
            {
                pictures = new List<Picture>();

                pictures = PictureManager.GetPicturesByRoom(id);
            }

            RoomDetailsViewModel roomDetailsViewModel = new RoomDetailsViewModel(room, pictures);

            return View(roomDetailsViewModel);
        }

    }
}
