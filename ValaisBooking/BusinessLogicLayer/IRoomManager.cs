using DataTransferObject;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    /// <summary>
    /// The RoomManager Interface for managing rooms
    /// </summary>
    public interface IRoomManager
    {
        /// <summary>
        ///     Retrieves the available rooms for the given hotel id, checkIn and checkOut dates, and advanced filters
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
        ///     A list of rooms
        /// </returns>
        List<Room> GetAvailableRoomsByHotel(int idHotel, DateTime checkIn, DateTime checkOut, bool hasTV, bool hasHairDryer, int minPrice, int maxPrice, bool simpleRoom, bool doubleRoom);

        /// <summary>
        ///     Retrieves the room for the given room id
        /// </summary>
        /// <param name="idRoom">
        ///     The room id
        /// </param>
        /// <returns>
        ///     A room
        /// </returns>
        Room GetRoom(int idRoom);

        /// <summary>
        ///     Retrieves the rooms for the given hotel id
        /// </summary>
        /// <param name="idHotel">
        ///     The hotel id
        /// </param>
        /// <returns>
        ///     A list of rooms
        /// </returns>
        List<Room> GetRoomsByHotel(int idHotel);
    }
}