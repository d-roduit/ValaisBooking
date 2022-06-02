using DataTransferObject;
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// <summary>
    /// The HotelDB Interface for accessing hotels data from database
    /// </summary>
    public interface IHotelDB
    {
        /// <summary>
        ///     Retrieves all hotels
        /// </summary>
        /// <returns>
        ///     A list of hotels
        /// </returns>
        List<Hotel> GetAllHotels();

        /// <summary>
        ///     Retrieves the hotel for the given hotel id
        /// </summary>
        /// <param name="idHotel">
        ///     The hotel id
        /// </param>
        /// <returns>
        ///     An hotel
        /// </returns>
        Hotel GetHotel(int idHotel);

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
        List<Hotel> GetHotels(string destination, DateTime checkIn, DateTime checkOut, string filtersString);

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
        bool IsOccupancyOver(int occupancyPercentage, int idHotel, DateTime checkIn, DateTime checkOut);
    }
}