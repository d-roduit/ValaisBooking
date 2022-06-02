using DataTransferObject;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    /// <summary>
    /// The HotelManager Interface for managing hotels
    /// </summary>
    public interface IHotelManager
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
        ///     Retrieves the hotels found for the given destination, checkIn and checkOut dates, and advanced filters
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
        /// <param name="hasWifi">
        ///     The hotel has wifi filter
        /// </param>
        /// <param name="hasParking">
        ///     The hotel has a parking filter
        /// </param>
        /// <param name="categories">
        ///     The hotel category filter
        /// </param>
        /// <param name="hasTV">
        ///     The room has a TV filter
        /// </param>
        /// <param name="hasHairDryer">
        ///     The room has a hair dryer filter
        /// </param>
        /// <param name="minPrice">
        ///     The room minimum price filter
        /// </param>
        /// <param name="maxPrice">
        ///     The room maximum price filter
        /// </param>
        /// <param name="simpleRoom">
        ///     The room has simple rooms filter
        /// </param>
        /// <param name="doubleRoom">
        ///     The room has double rooms filter
        /// </param>
        /// <returns>
        ///     A list of hotels
        /// </returns>
        List<Hotel> GetHotels(string destination, DateTime checkIn, DateTime checkOut, bool hasWifi, bool hasParking, List<bool> categories, bool hasTV, bool hasHairDryer, int minPrice, int maxPrice, bool simpleRoom, bool doubleRoom);

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