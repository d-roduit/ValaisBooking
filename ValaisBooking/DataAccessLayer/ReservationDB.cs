using DataTransferObject;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace DataAccessLayer
{
    /// <summary>
    /// The ReservationDB class
    /// Contains all methods for handling reservations data from the database
    /// </summary>
    public class ReservationDB : IReservationDB
    {
        /// <value>
        /// The Configuartion Interface
        /// </value>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The ReservationDB constructor
        /// </summary>
        /// <param name="configuration">
        ///     The Configuration Interface
        /// </param>
        public ReservationDB(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        ///     Add a reservation with the given reservation number, firstname, lastname, checkin and checkout dates and the room id.
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
            int nbRowsAffected = -1;

            try
            {
                using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ValaisBookingDB"));
                string query = "INSERT INTO Reservation (ReservationNumber, Firstname, Lastname, CheckIn, CheckOut, Room_IdRoom) VALUES (@reservationNumber, @firstname, @lastname, @checkIn, @checkOut, @room_IdRoom)";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@reservationNumber", reservationNumber);
                sqlCommand.Parameters.AddWithValue("@firstname", firstname);
                sqlCommand.Parameters.AddWithValue("@lastname", lastname);
                sqlCommand.Parameters.AddWithValue("@checkIn", checkIn);
                sqlCommand.Parameters.AddWithValue("@checkOut", checkOut);
                sqlCommand.Parameters.AddWithValue("@room_IdRoom", room_IdRoom);

                connection.Open();

                nbRowsAffected = (int)sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

            return nbRowsAffected;
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
                using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ValaisBookingDB"));

                string query = "SELECT * FROM Reservation WHERE ReservationNumber = @reservationNumber AND Firstname = @firstname AND Lastname = @lastname";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@reservationNumber", reservationNumber);
                cmd.Parameters.AddWithValue("@firstname", firstname);
                cmd.Parameters.AddWithValue("@lastname", lastname);

                connection.Open();

                using SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (reservations == null)
                    {
                        reservations = new List<Reservation>();
                    }

                    int idReservation = (int)dataReader["IdReservation"];
                    DateTime checkIn = (DateTime)dataReader["CheckIn"];
                    DateTime checkOut = (DateTime)dataReader["CheckOut"];
                    int room_IdRoom = (int)dataReader["Room_IdRoom"];


                    Reservation reservation = new Reservation(idReservation, reservationNumber, firstname, lastname, checkIn, checkOut, room_IdRoom);
                    reservations.Add(reservation);
                }
            }
            catch (Exception e)
            {
                throw e;
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
                using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ValaisBookingDB"));

                
                string query = "DELETE FROM Reservation WHERE IdReservation=@idReservation";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@idReservation", idReservation);

                connection.Open();

                result = cmd.ExecuteNonQuery();
                
            }
            catch (Exception e)
            {
                throw e;
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
                using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ValaisBookingDB"));


                string query = "SELECT MAX(ReservationNumber) FROM Reservation";
                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();

                maxReservationNumber = (int)cmd.ExecuteScalar();

            }
            catch (Exception e)
            {
                throw e;
            }

            return maxReservationNumber;
        }

    }
}
