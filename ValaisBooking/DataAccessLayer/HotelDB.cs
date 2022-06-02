using DataTransferObject;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// The HotelDB class
    /// Contains all methods for retrieving hotels data from the database
    /// </summary>
    public class HotelDB : IHotelDB
    {
        /// <value>
        /// The Configuartion Interface
        /// </value>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The HotelDB constructor
        /// </summary>
        /// <param name="configuration">
        ///     The Configuration Interface
        /// </param>
        public HotelDB(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        /// <summary>
        ///     Retrieves all hotels
        /// </summary>
        /// <returns>
        ///     A list of hotels
        /// </returns>
        public List<Hotel> GetAllHotels()
        {
            List<Hotel> hotels = null;

            try
            {
                using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ValaisBookingDB"));
                string query = "SELECT * FROM Hotel";
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                connection.Open();

                using SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    if (hotels == null)
                    {
                        hotels = new List<Hotel>();
                    }

                    int idHotel = (int)dataReader["IdHotel"];
                    string name = (string)dataReader["Name"];
                    string description = (string)dataReader["Description"];
                    string location = (string)dataReader["Location"];
                    int category = (int)dataReader["Category"];
                    bool hasWifi = (bool)dataReader["HasWifi"];
                    bool hasParking = (bool)dataReader["HasParking"];
                    string phone = (string)dataReader["Phone"];
                    string email = (string)dataReader["Email"];
                    string website = (string)dataReader["Website"];

                    Hotel hotel = new Hotel(idHotel, name, description, location, category, hasWifi, hasParking, phone, email, website);
                    hotels.Add(hotel);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return hotels;
        }


        /// <summary>
        ///     Retrieves the hotel for the given hotel id
        /// </summary>
        /// <param name="idHotel">
        ///     The hotel id
        /// </param>
        /// <returns>
        ///     An hotel
        /// </returns>
        public Hotel GetHotel(int idHotel)
        {
            Hotel hotel = null;

            try
            {
                using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ValaisBookingDB"));
                string query = "SELECT * FROM Hotel WHERE IdHotel = @idHotel";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@idHotel", idHotel);

                connection.Open();

                using SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    string name = (string)dataReader["Name"];
                    string description = (string)dataReader["Description"];
                    string location = (string)dataReader["Location"];
                    int category = (int)dataReader["Category"];
                    bool hasWifi = (bool)dataReader["HasWifi"];
                    bool hasParking = (bool)dataReader["HasParking"];
                    string phone = (string)dataReader["Phone"];
                    string email = (string)dataReader["Email"];
                    string website = (string)dataReader["Website"];

                    hotel = new Hotel(idHotel, name, description, location, category, hasWifi, hasParking, phone, email, website);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return hotel;
        }


        /// <summary>
        ///     Retrieves the hotels found for the given destination, checkIn and checkOut dates, and advanced filters string
        /// </summary>
        /// <param name="destination">
        ///     The name of an hotel (partial or exact) or the name of a city
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
        ///     A list of hotels
        /// </returns>
        public List<Hotel> GetHotels(string destination, DateTime checkIn, DateTime checkOut, string filtersString)
        {
            List<Hotel> hotels = null;

            try
            {
                using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ValaisBookingDB"));

                string query = "SELECT Hotel.IdHotel, Hotel.Name, Hotel.Description, Hotel.Location, Hotel.Category, Hotel.HasWifi, Hotel.HasParking, Hotel.Phone, Hotel.Email, Hotel.Website FROM Hotel" +
                    " INNER JOIN Room ON Hotel.IdHotel = Room.Hotel_IdHotel" +
                    " LEFT JOIN Reservation ON Room.IdRoom = Reservation.Room_IdRoom" +
                    " AND ((@checkIn < Reservation.CheckOut) AND (@checkOut > Reservation.CheckIn))" +
                    " WHERE Reservation.Room_IdRoom IS NULL" +
                    " AND (Location LIKE '%' + @destination + '%' OR Name LIKE '%' + @destination + '%')" +
                    filtersString +
                    " GROUP BY Hotel.IdHotel, Hotel.Name, Hotel.Description, Hotel.Location, Hotel.Category, Hotel.HasWifi, Hotel.HasParking, Hotel.Phone, Hotel.Email, Hotel.Website";

                SqlCommand sqlCommand = new SqlCommand(query, connection);

                sqlCommand.Parameters.AddWithValue("@destination", destination);
                sqlCommand.Parameters.AddWithValue("@checkIn", checkIn);
                sqlCommand.Parameters.AddWithValue("@checkOut", checkOut);

                connection.Open();

                using SqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    if (hotels == null)
                    {
                        hotels = new List<Hotel>();
                    }

                    int idHotel = (int)dataReader["IdHotel"];
                    string name = (string)dataReader["Name"];
                    string description = (string)dataReader["Description"];
                    string location = (string)dataReader["Location"];
                    int category = (int)dataReader["Category"];
                    bool hasWifi = (bool)dataReader["HasWifi"];
                    bool hasParking = (bool)dataReader["HasParking"];
                    string phone = (string)dataReader["Phone"];
                    string email = (string)dataReader["Email"];
                    string website = (string)dataReader["Website"];

                    Hotel hotel = new Hotel(idHotel, name, description, location, category, hasWifi, hasParking, phone, email, website);

                    hotels.Add(hotel);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return hotels;
        }

        /// <summary>
        ///     Checks if the booked rooms of an hotel is more than a given percentage for the given occupancy percentage, hotel id and checkIn and checkOut dates
        /// </summary>
        /// <param name="occupancyPercentage">
        ///     The occupancy percentage that the rooms booked should not exceed
        /// </param>
        /// <param name="idHotel">
        ///     The hotel id
        /// </param
        /// <param name="checkIn">
        ///     The arrival date
        /// </param>
        /// <param name="checkOut">
        ///     The departure date
        /// </param>
        /// <returns>
        ///     A bool
        /// </returns>
        public bool IsOccupancyOver(int occupancyPercentage, int idHotel, DateTime checkIn, DateTime checkOut)
        {
            bool isOccupancyOver = false;

            try
            {
                using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ValaisBookingDB"));

                string query = "SELECT" +
                    " CASE" +
                        " WHEN(" +
                            " CAST((" +
                                " SELECT COUNT(DISTINCT(Room.IdRoom))" +
                                " FROM Room INNER JOIN Reservation" +
                                    " ON Room.IdRoom = Reservation.Room_IdRoom" +
                                    " AND((@checkIn < Reservation.CheckOut) AND (@checkOut > Reservation.CheckIn))" +
                                    " AND Hotel_IdHotel = @idHotel" +
                            " ) AS FLOAT) / (SELECT COUNT(*) FROM Room WHERE Hotel_IdHotel = @idHotel)) * 100 >= @occupancyPercentage THEN CAST(1 AS BIT)" +
                        " ELSE CAST(0 AS BIT)" +
                    " END";

                SqlCommand sqlCommand = new SqlCommand(query, connection);

                sqlCommand.Parameters.AddWithValue("@checkIn", checkIn);
                sqlCommand.Parameters.AddWithValue("@checkOut", checkOut);
                sqlCommand.Parameters.AddWithValue("@occupancyPercentage", occupancyPercentage);
                sqlCommand.Parameters.AddWithValue("@idHotel", idHotel);

                connection.Open();

                isOccupancyOver = (bool)sqlCommand.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }

            return isOccupancyOver;
        }
    }
}
