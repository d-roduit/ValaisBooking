using DataAccessLayer;
using DataTransferObject;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    /// <summary>
    /// The ReservationManager class for managing the reservations
    /// </summary>
    public class ReservationManager : IReservationManager
    {
        /// <value>
        /// The Reservation Interface of Data Access Layer
        /// </value>
        private IReservationDB ReservationDB { get; }

        /// <summary>
        /// The ReservationManager constructor
        /// </summary>
        /// <param name="reservationDB">
        ///     The Reservation Interface of Data Access Layer
        /// </param>
        public ReservationManager(IReservationDB reservationDB)
        {
            ReservationDB = reservationDB;
        }

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
        public int AddReservation(int reservationNumber, string firstname, string lastname, DateTime checkIn, DateTime checkOut, int room_IdRoom)
        {
            int idReservation = -1;

            try
            {
                idReservation = ReservationDB.AddReservation(reservationNumber, firstname, lastname, checkIn, checkOut, room_IdRoom);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return idReservation;
        }

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
        public List<Reservation> GetReservations(int reservationNumber, string firstname, string lastname)
        {
            List<Reservation> reservations = null;

            try
            {
                reservations = ReservationDB.GetReservations(reservationNumber, firstname, lastname);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return reservations;
        }

        /// <summary>
        ///     Delete a reservation for the given reservation id
        /// </summary>
        /// <param name="idReservation">
        ///     The reservation id
        /// </param>
        /// <returns>
        ///     The number of lines affected 
        /// </returns>
        public int CancelReservation(int idReservation)
        {
            int result = 0;

            try
            {
                result = ReservationDB.CancelReservation(idReservation);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return result;
        }

        /// <summary>
        ///     Retrieves the biggest reservation number
        /// </summary>
        /// <returns>
        ///     The biggest reservation number
        /// </returns>
        public int GetMaxReservationNumber()
        {
            int maxReservationNumber = 0;

            try
            {
                maxReservationNumber = ReservationDB.GetMaxReservationNumber();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return maxReservationNumber;
        }

    }
}
