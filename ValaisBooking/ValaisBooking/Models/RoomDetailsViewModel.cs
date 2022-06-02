using DataTransferObject;
using Microsoft.AspNetCore.Html;
using System.Collections.Generic;

namespace ValaisBooking.Models
{
    /// <summary>
    ///     The RoomDetailsViewModel class wraps the room information in a model to be able to display or use them easily
    /// </summary>
    public class RoomDetailsViewModel
    {

        /// <summary>
        ///     Constructor of the RoomDetailsViewModel class
        /// </summary>
        public RoomDetailsViewModel() { }

        /// <summary>
        ///     Constructor of the RoomDetailsViewModel class
        /// </summary>
        /// <param name="room">
        ///     A Room object
        /// </param>
        /// <param name="pictures">
        ///     A list of room pictures
        /// </param>
        public RoomDetailsViewModel(Room room, List<Picture> pictures)
        {
            Room = room;
            Pictures = pictures;
        }

        /// <value>A Room object</value>
        public Room Room { get; set; }

        /// <value>A list of room pictures</value>
        public List<Picture> Pictures { get; set; }


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
