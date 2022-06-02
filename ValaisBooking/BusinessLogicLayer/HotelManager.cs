using DataAccessLayer;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer
{
    /// <summary>
    /// The HotelManager class for managing the hotels
    /// </summary>
    public class HotelManager : IHotelManager
    {
        /// <value>
        /// The Hotel Interface of Data Access Layer
        /// </value>
        private IHotelDB HotelDB { get; }

        /// <summary>
        /// The HotelManager constructor
        /// </summary>
        /// <param name="hotelDB">
        ///     The Hotel Interface of Data Acces Layer
        /// </param>
        public HotelManager(IHotelDB hotelDB)
        {
            HotelDB = hotelDB;
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
                hotels = HotelDB.GetAllHotels();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                hotel = HotelDB.GetHotel(idHotel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return hotel;
        }

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
        public List<Hotel> GetHotels(string destination, DateTime checkIn, DateTime checkOut, bool hasWifi, bool hasParking, List<bool> categories, bool hasTV, bool hasHairDryer, int minPrice, int maxPrice, bool simpleRoom, bool doubleRoom)
        {
            StringBuilder filtersStringBuilder = new StringBuilder(" ");

            if (hasWifi)
                filtersStringBuilder.Append("AND Hotel.HasWifi = 1 ");

            if (hasParking)
                filtersStringBuilder.Append("AND Hotel.HasParking = 1 ");

            bool categoriesHasTrueValues = false;
            foreach (bool category in categories)
            {
                if (category)
                    categoriesHasTrueValues = true;
            }

            if (categoriesHasTrueValues)
                filtersStringBuilder.Append("AND Hotel.Category IN (");

            int categoriesAppendedCount = 0;
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i])
                {
                    if (categoriesAppendedCount > 0)
                    {
                        filtersStringBuilder.Append(",");
                    }

                    filtersStringBuilder.Append(i+1);

                    categoriesAppendedCount++;
                }
            }

            if (categoriesHasTrueValues)
                filtersStringBuilder.Append(") ");

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

            List<Hotel> hotels = null;

            try
            {
                if (destination != null && checkIn != null && checkOut != null)
                {
                    hotels = HotelDB.GetHotels(destination, checkIn, checkOut, filtersString);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                isOccupancyOver = HotelDB.IsOccupancyOver(occupancyPercentage, idHotel, checkIn, checkOut);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return isOccupancyOver;
        }
    }
}
