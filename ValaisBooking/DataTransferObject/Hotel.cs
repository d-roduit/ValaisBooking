using System.ComponentModel.DataAnnotations;

namespace DataTransferObject
{
    /// <summary>
    /// The Hotel class represents an hotel object
    /// </summary>
    public class Hotel
    {
        /// <value>
        /// The hotel id
        /// </value>
        public int IdHotel { get; }

        /// <value>
        /// The hotel name
        /// </value>
        public string Name { get; }

        /// <value>
        /// The hotel description
        /// </value>
        public string Description { get; }

        /// <value>
        /// The hotel location
        /// </value>
        public string Location { get; }

        /// <value>
        /// The hotel category (number of stars)
        /// </value>
        public int Category { get; }

        /// <value>
        /// The hotel has wifi
        /// </value>
        [Display(Name = "Wifi")]
        public bool HasWifi { get; }

        /// <value>
        /// The hotel has a parking
        /// </value>
        [Display(Name = "Parking")]
        public bool HasParking { get; }

        /// <value>
        /// The hotel phone number
        /// </value>
        public string Phone { get; }

        /// <value>
        /// The hotel email
        /// </value>
        public string Email { get; }

        /// <value>
        /// The hotel website
        /// </value>
        public string Website { get; }

        /// <summary>
        /// The Hotel constructor
        /// </summary>
        /// <param name="idHotel">
        ///     The hotel id
        /// </param>
        /// <param name="name">
        ///     The hotel name
        /// </param>
        /// <param name="description">
        ///     The hotel description
        /// </param>
        /// <param name="location">
        ///     The hotel location
        /// </param>
        /// <param name="category">
        ///     The hotel category
        /// </param>
        /// <param name="hasWifi">
        ///     The hotel has wifi
        /// </param>
        /// <param name="hasParking">
        ///     The hotel has a parking
        /// </param>
        /// <param name="phone">
        ///     The hotel phone number
        /// </param>
        /// <param name="email">
        ///     The hotel email
        /// </param>
        /// <param name="website">
        ///     The hotel website
        /// </param>
        public Hotel(int idHotel, string name, string description, string location, int category, bool hasWifi, bool hasParking, string phone, string email, string website)
        {
            IdHotel = idHotel;
            Name = name;
            Description = description;
            Location = location;
            Category = category;
            HasWifi = hasWifi;
            HasParking = hasParking;
            Phone = phone;
            Email = email;
            Website = website;
        }

        /// <summary>
        ///     Display the hotel object as a string
        ///     Override of the parent ToString() method
        /// </summary>
        /// <returns>
        ///     The hotel object as a string
        /// </returns>
        public override string ToString()
        {
            return $"Hotel {{ {nameof(IdHotel)}: {IdHotel}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description.Substring(0, 20)}..., {nameof(Location)}: {Location}, {nameof(Category)}: {Category}, {nameof(HasWifi)}: {HasWifi}, {nameof(HasParking)}: {HasParking}, {nameof(Phone)}: {Phone}, {nameof(Email)}: {Email}, {nameof(Website)}: {Website} }}";
        }
    }
}
