using DataAccessLayer;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer
{
    /// <summary>
    /// The RoomManager class for managing the rooms
    /// </summary>
    public class RoomManager : IRoomManager
    {
        /// <value>
        /// The Room Interface of Data Access Layer
        /// </value>
        private IRoomDB RoomDB { get; }

        /// <summary>
        /// The RoomManager constructor
        /// </summary>
        /// <param name="roomDB">
        ///     The Room Interface of Data Acces Layer
        /// </param>
        public RoomManager(IRoomDB roomDB)
        {
            RoomDB = roomDB;
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
                rooms = RoomDB.GetRoomsByHotel(idHotel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return rooms;
        }

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
        public List<Room> GetAvailableRoomsByHotel(int idHotel, DateTime checkIn, DateTime checkOut, bool hasTV, bool hasHairDryer, int minPrice, int maxPrice, bool simpleRoom, bool doubleRoom)
        {
            StringBuilder filtersStringBuilder = new StringBuilder(" ");

            if (hasTV)
                filtersStringBuilder.Append("AND Room.HasTV = 1 ");

            if (hasHairDryer)
                filtersStringBuilder.Append("AND Room.HasHairDryer = 1 ");

            filtersStringBuilder.Append("AND Room.Price BETWEEN " + minPrice + " AND " + maxPrice + " ");

            if (simpleRoom && doubleRoom)
            {
                filtersStringBuilder.Append("AND Room.Type IN (1,2) ");
            }
            else
            {
                if (simpleRoom)
                    filtersStringBuilder.Append("AND Room.Type = 1 ");

                if (doubleRoom)
                    filtersStringBuilder.Append("AND Room.Type = 2 ");
            }

            string filtersString = filtersStringBuilder.ToString();


            List<Room> rooms = null;

            try
            {
                rooms = RoomDB.GetAvailableRoomsByHotel(idHotel, checkIn, checkOut, filtersString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                room = RoomDB.GetRoom(idRoom);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return room;
        }
    }
}
