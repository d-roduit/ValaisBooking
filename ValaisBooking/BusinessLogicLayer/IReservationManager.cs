using DataTransferObject;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    /// <summary>
    /// The ReservationManager Interface for managing reservations
    /// </summary>
    public interface IReservationManager
    {
        /// <summary>
        ///     Add a reservation with the given reservation number, firstname, lastname, checkin and checkout dates and the room id
        /// </summary>
        /// <param name="reservationNumber">
        ///     The reservation number
        /// </param>
        /// <param name="firstname">
        ///     The firstname of the person reserving
        /// </param>
        /// <param name="lastname">
        ///     The lastname of the person reserving
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
        /// <returns>
        ///     The number of rows affected
        /// </returns>
        int AddReservation(int reservationNumber, string firstname, string lastname, DateTime checkIn, DateTime checkOut, int room_IdRoom);

        /// <summary>
        ///     Retrieves a list of reservations for the given reservation number, firstname and lastname
        /// </summary>
        /// <param name="reservationNumber">
        ///     The reservation number
        /// </param>
        /// <param name="firstname">
        ///     The firstname of the person that made the reservation
        /// </param>
        /// <param name="lastname">
        ///     The lastname of the person that made the reservation
        /// </param>
        /// <returns>
        ///     A list of reservations 
        /// </returns>
        List<Reservation> GetReservations(int reservationNumber, string firstname, string lastname);

        /// <summary>
        ///     Delete a reservation for the given reservation id
        /// </summary>
        /// <param name="idReservation">
        ///     The reservation id
        /// </param>
        /// <returns>
        ///     The number of lines affected 
        /// </returns>
        int CancelReservation(int idReservation);

        /// <summary>
        ///     Retrieves the biggest reservation number
        /// </summary>
        /// <returns>
        ///     The biggest reservation number
        /// </returns>
        int GetMaxReservationNumber();
    }
}