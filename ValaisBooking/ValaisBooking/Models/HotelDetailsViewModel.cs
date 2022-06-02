using DataTransferObject;
using Microsoft.AspNetCore.Html;
using System.Collections.Generic;

namespace ValaisBooking.Models
{
    /// <summary>
    ///     The HotelDetailsViewModel class wraps the hotel information in a model to be able to easily display or use them
    /// </summary>
    public class HotelDetailsViewModel
    {
        /// <summary>
        ///     Constructor of the HotelDetailsViewModel class
        /// </summary>
        public HotelDetailsViewModel() { }

        /// <summary>
        ///     Constructor of the HotelDetailsViewModel class
        /// </summary>
        /// <param name="hotel">
        ///     A Hotel object
        /// </param>
        /// <param name="rooms">
        ///     A list of Room objects
        /// </param>
        /// <param name="picturesByRoom">
        ///     A list of list of room pictures
        /// </param>
        public HotelDetailsViewModel(Hotel hotel, List<Room> rooms, List<List<Picture>> picturesByRoom)
        {
            Hotel = hotel;
            Rooms = rooms;
            PicturesByRoom = picturesByRoom;
        }

        /// <value>A Hotel object</value>
        public Hotel Hotel { get; set; }

        /// <value>A list of Room objects</value>
        public List<Room> Rooms { get; set; }

        /// <value>A list of list of room pictures</value>
        public List<List<Picture>> PicturesByRoom { get; set; }

        /// <summary>
        ///     Format the Type property of the given room to a meaningful text format
        /// </summary>
        /// <param name="room">
        ///     A Room object
        /// </param>
        /// <returns>
        ///     The Type property as a meaningful text
        /// </returns>
        public static string DisplayTextForRoomType(Room room)
        {
            string displayText = room.Type switch
            {
                1 => "Simple room",
                2 => "Double room",
                _ => "Big room",
            };

            return displayText;
        }

        /// <summary>
        ///     Format the HasParking property of the given hotel to a displayable HTML string format
        /// </summary>
        /// <param name="hotel">
        ///     A Hotel object
        /// </param>
        /// <returns>
        ///     The HasParking property as a displayable HTML string
        /// </returns>
        public static HtmlString DisplayHTMLForHasParking(Hotel hotel)
        {
            HtmlString displayHTML;

            if(hotel.HasParking)
            {
                displayHTML = new HtmlString("<span class=\"text-success\"><i class=\"fas fa-parking\" style=\"font-size: 20px;\"></i> Free parking</span>");
            }
            else
            {
                displayHTML = new HtmlString("<span class=\"text-danger\"><i class=\"fas fa-parking\" style=\"font-size: 20px;\"></i> No parking</span>");
            }

            return displayHTML;
        }


        /// <summary>
        ///     Format the HasWifi property of the given hotel to a displayable HTML string format
        /// </summary>
        /// <param name="hotel">
        ///     A Hotel object
        /// </param>
        /// <returns>
        ///     The HasWifi property as a displayable HTML string
        /// </returns>
        public static HtmlString DisplayHTMLForHasWifi(Hotel hotel)
        {
            HtmlString displayHTML;

            if (hotel.HasWifi)
            {
                displayHTML = new HtmlString("<span class=\"text-success\"><i class=\"fas fa-wifi\"></i> Free Wi-Fi connection</span>");
            }
            else
            {
                displayHTML = new HtmlString("<span class=\"text-danger\"><i class=\"fas fa-wifi\"></i> No Wi-Fi connection</span>");
            }

            return displayHTML;
        }

        /// <summary>
        ///     Format the HasTV property of the given room to a displayable HTML string format
        /// </summary>
        /// <param name="room">
        ///     A Room object
        /// </param>
        /// <returns>
        ///     The HasTV property as a displayable HTML string
        /// </returns>
        public static HtmlString DisplayHTMLForHasTV(Room room)
        {
            HtmlString displayHTML;

            if (room.HasTV)
            {
                displayHTML = new HtmlString("<span class=\"text-success\"><i class=\"fas fa-tv\"></i> Flat screen TV</span>");
            }
            else
            {
                displayHTML = new HtmlString("<span class=\"text-danger\"><i class=\"fas fa-tv\"></i> No TV</span>");
            }

            return displayHTML;
        }

        /// <summary>
        ///     Format the HasHairDryer property of the given room to a displayable HTML string format
        /// </summary>
        /// <param name="room">
        ///     A Room object
        /// </param>
        /// <returns>
        ///     The HasHairDryer property as a displayable HTML string
        /// </returns>
        public static HtmlString DisplayHTMLForHasHairDryer(Room room)
        {
            HtmlString displayHTML;

            if (room.HasHairDryer)
            {
                displayHTML = new HtmlString("<span class=\"text-success\"><i class=\"fas fa-wind\"></i> Hair Dryer</span>");
            }
            else
            {
                displayHTML = new HtmlString("<span class=\"text-danger\"><i class=\"fas fa-wind\"></i> No Hair Dryer</span>");
            }

            return displayHTML;
        }

    }
}