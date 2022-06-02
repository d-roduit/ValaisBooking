using DataTransferObject;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer

{
    /// <summary>
    /// The RoomDB class
    /// Contains all methods for retrieving rooms data from the database
    /// </summary>
    public class RoomDB : IRoomDB
    {
        /// <value>
        /// The Configuartion Interface
        /// </value>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The RoomDB constructor
        /// </summary>
        /// <param name="configuration">
        ///     The Configuration Interface
        /// </param>
        public RoomDB(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        ///     Retrieves the rooms for the given hotel id
        /// </summary>
        /// <param name="idHotel">
        ///     The hotel id
        /// </param>
        /// <returns>
        ///     A list of rooms
        /// </returns>
        public List<Room> GetRoomsByHotel(int idHotel)
        {
            List<Room> rooms = null;

            try
            {
                using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ValaisBookingDB"));

                string query = "SELECT * FROM Room WHERE Hotel_IdHotel=@idHotel";
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                sqlCommand.Parameters.AddWithValue("@idHotel", idHotel);

                connection.Open();

                using SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    if (rooms == null)
                    {
                        rooms = new List<Room>();
                    }

                    int idRoom = (int)dataReader["IdRoom"];
                    int number = (int)dataReader["Number"];
                    string description = (string)dataReader["Description"];
                    int type = (int)dataReader["Type"];
                    decimal price = (decimal)dataReader["Price"];
                    bool hasTV = (bool)dataReader["HasTV"];
                    bool hasHairDryer = (bool)dataReader["HasHairDryer"];

                    Room room = new Room(idRoom, number, description, type, price, hasTV, hasHairDryer, idHotel);

                    rooms.Add(room);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return rooms;
        }

        /// <summary>
        ///     Retrieves the available rooms for the given hotel id, checkIn and checkOut dates, and advanced filters string
        /// </summary>
        /// <param name="idHotel">
        ///     The hotel id
        /// </param>
        /// <param name="checkIn">
        ///     The arrival date
        /// </param>
        /// <param name="checkOut">
        ///     The departure date
        /// </param>
        /// <param name="filtersString">
        ///     The string containing the advanced filters that are applied
        /// </param>
        /// <returns>
        ///     A list of rooms
        /// </returns>
        public List<Room> GetAvailableRoomsByHotel(int idHotel, DateTime checkIn, DateTime checkOut, string filtersString)
        {
            List<Room> rooms = null;

            try
            {
                using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ValaisBookingDB"));

                string query = "SELECT * FROM Room" +
                    " LEFT JOIN Reservation ON Room.IdRoom = Reservation.Room_IdRoom" +
                    " AND ((@checkIn < Reservation.CheckOut)  AND (@checkOut > Reservation.CheckIn))" +
                    " WHERE Reservation.Room_IdRoom IS NULL" +
                    " AND Hotel_IdHotel = @idHotel" +
                    filtersString;

                SqlCommand sqlCommand = new SqlCommand(query, connection);

                sqlCommand.Parameters.AddWithValue("@checkIn", checkIn);
                sqlCommand.Parameters.AddWithValue("@checkOut", checkOut);
                sqlCommand.Parameters.AddWithValue("@idHotel", idHotel);

                connection.Open();

                using SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    if (rooms == null)
                    {
                        rooms = new List<Room>();
                    }

                    int idRoom = (int)dataReader["IdRoom"];
                    int number = (int)dataReader["Number"];
                    string description = (string)dataReader["Description"];
                    int type = (int)dataReader["Type"];
                    decimal price = (decimal)dataReader["Price"];
                    bool hasTV = (bool)dataReader["HasTV"];
                    bool hasHairDryer = (bool)dataReader["HasHairDryer"];

                    Room room = new Room(idRoom, number, description, type, price, hasTV, hasHairDryer, idHotel);

                    rooms.Add(room);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return rooms;
        }

        /// <summary>
        ///     Retrieves the room for the given room id
        /// </summary>
        /// <param name="idRoom">
        ///     The room id
        /// </param>
        /// <returns>
        ///     A room
        /// </returns>
        public Room GetRoom(int idRoom)
        {
            Room room = null;

            try
            {
                using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ValaisBookingDB"));

                string query = "SELECT * FROM Room WHERE IdRoom = @idRoom";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@idRoom", idRoom);

                connection.Open();

                using SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    int number = (int)dataReader["Number"];
                    string description = (string)dataReader["Description"];
                    int type = (int)dataReader["Type"];
                    decimal price = (decimal)dataReader["Price"];
                    bool hasTV = (bool)dataReader["HasTV"];
                    bool hasHairDryer = (bool)dataReader["HasHairDryer"];
                    int hotel_IdHotel = (int)dataReader["Hotel_IdHotel"];


                    room = new Room(idRoom, number, description, type, price, hasTV, hasHairDryer, hotel_IdHotel);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return room;
        }


    }
}
