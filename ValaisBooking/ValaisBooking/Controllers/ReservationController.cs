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
    /// The ReservationController class manages URLs relating to the reservation <c>/Reservation/[...]</c>
    /// </summary>
    public class ReservationController : Controller
    {
        /// <value>The ReservationManager object from the Business Logic Layer that retrieves all information relating to the reservations made</value>
        private IReservationManager ReservationManager { get; }

        /// <value>The RoomManager object from the Business Logic Layer that retrieves all information relating to the hotel rooms</value>
        private IRoomManager RoomManager { get; }

        /// <summary>
        ///     Constructor of the ReservationController class
        /// </summary>
        /// <param name="reservationManager">
        ///     The ReservationManager object from the Business Logic Layer that retrieves all information relating to the reservations made
        /// </param>
        /// <param name="roomManager">
        ///     The RoomManager object from the Business Logic Layer that retrieves all information relating to the rooms
        /// </param>
        public ReservationController(IReservationManager reservationManager, IRoomManager roomManager)
        {
            ReservationManager = reservationManager;
            RoomManager = roomManager;
        }

        /// <summary>
        ///     This method shows the Index View for the reservation
        /// </summary>
        /// <returns>
        ///     The Index View
        /// </returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     This method handles the validation of the form in the Index View
        /// </summary>
        /// <param name="reservation">
        ///     A reservation
        /// </param>
        /// <returns>
        ///     The Index View for the given reservation or redirect to the GetReservations method for the given reservation number, firstname and lastname
        /// </returns>
        [HttpPost]
        public IActionResult Index(Reservation reservation)
        {
            if (!String.IsNullOrEmpty(reservation.Firstname) && !String.IsNullOrEmpty(reservation.Lastname))
            {
                List<Reservation> reservations = ReservationManager.GetReservations(reservation.ReservationNumber, reservation.Firstname, reservation.Lastname);

                if (reservations == null)
                {
                    ModelState.AddModelError(string.Empty, "No reservation was found for these information");
                }
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("GetReservations", "Reservation", new { reservationNumber = reservation.ReservationNumber, firstname = reservation.Firstname, lastname = reservation.Lastname });
            }

            return View(reservation);
        }

        /// <summary>
        ///     This method retrieves a list of reservations for the given reservation number, firstname and lastname
        /// </summary>
        /// <param name="reservationNumber">
        ///     The reservation number
        /// </param>
        /// <param name="firstname">
        ///     The firstname of the person that made this reservation
        /// </param>
        /// <param name="lastname">
        ///     The lastname of the person that made this reservation
        /// </param>
        /// <returns>
        ///     The GetReservations View for the given GetReservations ViewModel
        /// </returns>
        [HttpGet]
        public ActionResult GetReservations(int reservationNumber, string firstname, string lastname)
        {
            if (TempData["deleteConfirmed"] != null)
            {
                ViewData["deleteConfirmed"] = true;
            }

            List<Reservation> reservations = ReservationManager.GetReservations(reservationNumber, firstname, lastname);

            List<Room> rooms = new List<Room>();

            if (reservations == null)
            {
                return RedirectToAction("Index");
            }

            foreach (Reservation reservation in reservations)
            {
                rooms.Add(RoomManager.GetRoom(reservation.Room_IdRoom));
            }

            GetReservationsViewModel getReservationViewModel = new GetReservationsViewModel(reservations, rooms);

            return View(getReservationViewModel);
        }


        /// <summary>
        ///     This method adds a reservation for the given room id
        /// </summary>
        /// <param name="idRoom">
        ///     The room id
        /// </param>
        /// <returns>
        ///     The PersonDetails View or the AddReservation View
        /// </returns>
        public ActionResult AddReservation(int idRoom)
        {
            Room room = RoomManager.GetRoom(idRoom);

            string firstname = HttpContext.Session.GetString("firstname");
            string lastname = HttpContext.Session.GetString("lastname");

            if (String.IsNullOrEmpty(firstname) || String.IsNullOrEmpty(lastname))
            {
                ViewData["idRoom"] = idRoom;
                return View("PersonDetails");
            }

            DateTime checkInSession = Convert.ToDateTime(HttpContext.Session.GetString("checkIn"));
            DateTime checkOutSession = Convert.ToDateTime(HttpContext.Session.GetString("checkOut"));

            DateTime checkInRes= Convert.ToDateTime(HttpContext.Session.GetString("checkInReservation"));
            DateTime checkOutRes = Convert.ToDateTime(HttpContext.Session.GetString("checkOutReservation"));

            if (HttpContext.Session.GetInt32("hotelBooked") == null || room.Hotel_IdHotel != (int)HttpContext.Session.GetInt32("hotelBooked") || checkInSession != checkInRes || checkOutSession != checkOutRes)
            {
                HttpContext.Session.SetInt32("numReservation", ReservationManager.GetMaxReservationNumber()+1);
            }

            HttpContext.Session.SetInt32("hotelBooked", room.Hotel_IdHotel);
            

            int numReservation = (int)HttpContext.Session.GetInt32("numReservation");
            DateTime checkInReservation = checkInSession;
            DateTime checkOutReservation = checkOutSession;
            HttpContext.Session.SetString("checkInReservation", checkInReservation.ToString());
            HttpContext.Session.SetString("checkOutReservation", checkOutReservation.ToString());

            ReservationManager.AddReservation(numReservation, firstname, lastname, checkInReservation, checkOutReservation, idRoom);

            return View();
        }

        /// <summary>
        ///     This method sets the firstname and lastname as sessions
        /// </summary>
        /// <param name="firstname">
        ///     The firstname of the person that is booking a room
        /// </param>
        /// <param name="lastname">
        ///     The lastname of the person that is booking a room
        /// </param>
        /// <param name="idRoom">
        ///     The room id
        /// </param>
        /// <returns>
        ///     Redirect to the AddReservation method with the room id
        /// </returns>
        [HttpPost]
        public ActionResult PersonSession(string firstname, string lastname, int idRoom)
        {
            HttpContext.Session.SetString("firstname", firstname);
            HttpContext.Session.SetString("lastname", lastname);

            return RedirectToAction("AddReservation", "Reservation", new { idRoom = idRoom });
        }

        /// <summary>
        ///     This method deletes a reservation for the given reservation id
        /// </summary>
        /// <param name="id">
        ///     The reservation id
        /// </param>
        /// <param name="numReservation">
        ///     The reservation number
        /// </param>
        /// <param name="firstname">
        ///     The firstname of the person that booked
        /// </param>
        /// <param name="lastname">
        ///     The lastname of the person that booked
        /// </param>
        /// <returns>
        ///     Redirect to the DeleteConfirmed method
        /// </returns>
        [HttpPost]
        public ActionResult Delete(int id, int numReservation, string firstname, string lastname)
        {
            TempData["deleteConfirmed"] = true;

            ReservationManager.CancelReservation(id);
            return RedirectToAction("GetReservations", "Reservation", new { reservationNumber = numReservation, firstname = firstname, lastname = lastname});
        }


    }
}
