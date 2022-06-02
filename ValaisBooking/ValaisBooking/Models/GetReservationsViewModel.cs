using DataTransferObject;
using System.Collections.Generic;

namespace ValaisBooking.Models
{
    /// <summary>
    ///     The GetReservationsViewModel class wraps the reservation information in a model to be able to easily display or use them
    /// </summary>
    public class GetReservationsViewModel
    {
        /// <summary>
        ///     Constructor of the GetReservationsViewModel class
        /// </summary>
        public GetReservationsViewModel() { }

        /// <summary>
        ///     Constructor of the GetReservationsViewModel class
        /// </summary>
        /// <param name="reservations">
        ///     A list of Reservation objects
        /// </param>
        /// <param name="rooms">
        ///     A list of Room objects
        /// </param>
        public GetReservationsViewModel(List<Reservation> reservations, List<Room> rooms)
        {
            Reservations = reservations;
            Rooms = rooms;
        }

        /// <value>A list of Reservation objects</value>
        public List<Reservation> Reservations { get; set; }

        /// <value>A list of Room objects</value>
        public List<Room> Rooms { get; set; }

        /// <summary>
        ///     Format the Type property of the given room to a meaningful text format
        /// </summary>
        /// <param name="room">
        ///     A Room object
        /// </param>
        /// <returns>
        ///     The Type property as a meaningful text
        /// </returns>
        public static string DisplayTextForRoomType(Room room)
        {
            string displayText = room.Type switch
            {
                1 => "Simple room",
                2 => "Double room",
                _ => "Big room",
            };

            return displayText;
        }

    }
}
