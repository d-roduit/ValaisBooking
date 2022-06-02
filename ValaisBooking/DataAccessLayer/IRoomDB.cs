using DataTransferObject;
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// <summary>
    /// The RoomDB Interface for accessing rooms data from database
    /// </summary>
    public interface IRoomDB
    {
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
        List<Room> GetAvailableRoomsByHotel(int idHotel, DateTime checkIn, DateTime checkOut, string filtersString);

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