using System;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObject
{
    /// <summary>
    /// The Reservation class represents a reservation object
    /// </summary>
    public class Reservation
    {
        /// <value>
        /// The reservation id
        /// </value>
        public int IdReservation { get; set; }

        /// <value>
        /// The reservation number
        /// </value>
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name ="Reservation Number")]
        public int ReservationNumber { get; set; }

        /// <value>
        /// The firstname of the person that booked
        /// </value>
        [Required(ErrorMessage = "{0} is required.")]
        public string Firstname { get; set; }

        /// <value>
        /// The lastname of the person that booked
        /// </value>
        [Required(ErrorMessage = "{0} is required.")]
        public string Lastname { get; set; }

        /// <value>
        /// The arrival date
        /// </value>
        [DataType(DataType.Date)]
        [Display(Name = "Check In")]
        public DateTime CheckIn { get; set; }

        /// <value>
        /// The departure date
        /// </value>
        [DataType(DataType.Date)]
        [Display(Name = "Check Out")]
        public DateTime CheckOut { get; set; }

        /// <value>
        /// The room id
        /// </value>
        public int Room_IdRoom { get; set; }

        /// <summary>
        /// The Reservation empty constructor
        /// </summary>
        public Reservation()
        {

        }

        /// <summary>
        /// The Reservation constructor
        /// </summary>
        /// <param name="idReservation">
        ///     The reservation id
        /// </param>
        /// <param name="reservationNumber">
        ///     The reservation number
        /// </param>
        /// <param name="firstName">
        ///     The firstname of the person that booked
        /// </param>
        /// <param name="lastName">
        ///     The lastname of the person that booked
        /// </param>
        /// <param name="checkIn">
        ///     The arrival date
        /// </param>
        /// <param name="checkOut">
        ///     The departure date
        /// </param>
        /// <param name="room_IdRoom">
        ///     The room id
        /// </param>
        public Reservation(int idReservation, int reservationNumber, string firstName, string lastName, DateTime checkIn, DateTime checkOut, int room_IdRoom)
        {
            IdReservation = idReservation;
            ReservationNumber = reservationNumber;
            Firstname = firstName;
            Lastname = lastName;
            CheckIn = checkIn;
            CheckOut = checkOut;
            Room_IdRoom = room_IdRoom;
        }

        /// <summary>
        ///     Display the reservation object as a string
        ///     Override of the parent ToString() method
        /// </summary>
        /// <returns>
        ///     The reservation object as a string
        /// </returns>
        public override string ToString()
        {
            return $"Reservation {{ {nameof(IdReservation)}: {IdReservation}, {nameof(ReservationNumber)}: {ReservationNumber}, {nameof(Firstname)}: {Firstname}, {nameof(Lastname)}: {Lastname}, {nameof(CheckIn)}: {CheckIn}, {nameof(CheckOut)}: {CheckOut}, {nameof(Room_IdRoom)}: {Room_IdRoom} }}";
        }
    }
}
