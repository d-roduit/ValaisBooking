using System.ComponentModel.DataAnnotations;

namespace DataTransferObject
{
    /// <summary>
    ///     The Room class represents a room
    /// </summary>
    public class Room
    {
        /// <value>The room id</value>
        public int IdRoom { get; set; }

        /// <value>The room number</value>
        [Display(Name = "Room Number")]
        public int Number { get; set; }

        /// <value>The room description</value>
        public string Description { get; set; }

        /// <value>The room type</value>
        [Display(Name = "Housing Type")]
        public int Type { get; set; }

        /// <value>The room price</value>
        public decimal Price { get; set; }

        /// <value>A boolean value to indicate if the room has a TV</value>
        [Display(Name = "TV")]
        public bool HasTV { get; set; }

        /// <value>A boolean value to indicate if the room has a hair dryer</value>
        [Display(Name = "Hair Dryer")]
        public bool HasHairDryer { get; set; }

        /// <value>The hotel id the room is related to</value>
        public int Hotel_IdHotel { get; set; }

        /// <summary>
        ///     Constructor of the Room class
        /// </summary>
        /// <param name="idRoom">
        ///     The room id
        /// </param>
        /// <param name="number">
        ///     The room number
        /// </param>
        /// <param name="description">
        ///     The room description
        /// </param>
        /// <param name="type">
        ///     The room type
        /// </param>
        /// <param name="price">
        ///     The room price
        /// </param>
        /// <param name="hasTV">
        ///     A boolean value to indicate if the room has a TV
        /// </param>
        /// <param name="hasHairDryer">
        ///     A boolean value to indicate if the room has a hair dryer
        /// </param>
        /// <param name="hotel_IdHotel">
        ///     The hotel id the room is related to
        /// </param>
        public Room(int idRoom, int number, string description, int type, decimal price, bool hasTV, bool hasHairDryer, int hotel_IdHotel)
        {
            IdRoom = idRoom;
            Number = number;
            Description = description;
            Type = type;
            Price = price;
            HasTV = hasTV;
            HasHairDryer = hasHairDryer;
            Hotel_IdHotel = hotel_IdHotel;
        }

        /// <summary>
        ///     Display the room object as a string
        ///     Override of the parent ToString() method
        /// </summary>
        /// <returns>
        ///     The room object as a string
        /// </returns>
        public override string ToString()
        {
            return $"Room {{ {nameof(IdRoom)}: {IdRoom}, {nameof(Number)}: {Number}, {nameof(Description)}: {Description.Substring(0, 30)}..., {nameof(Type)}: {Type}, {nameof(Price)}: {Price}, {nameof(HasTV)}: {HasTV}, {nameof(HasHairDryer)}: {HasHairDryer}, {nameof(Hotel_IdHotel)}: {Hotel_IdHotel} }}";
        }
    }
}
