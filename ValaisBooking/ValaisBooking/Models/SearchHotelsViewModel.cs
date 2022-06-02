using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ValaisBooking.Models
{
    /// <summary>
    ///     The SearchHotelsViewModel class wraps the search information to find hotels in a model to be able to easily display or use them
    /// </summary>
    public class SearchHotelsViewModel
    {
        /// <value>The location of the hotels the user wishes to consult</value>
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Text)]
        [MaxLengthAttribute(20)]
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        /// <value>The date the user wishes to arrive at the hotel</value>
        [DataType(DataType.Date)]
        [Display(Name = "Check In")]
        public DateTime CheckIn { get; set; }

        /// <value>The date the user wants to leave the hotel</value>
        [DataType(DataType.Date)]
        [Display(Name = "Check Out")]
        public DateTime CheckOut { get; set; }

        /// <value>A boolean value to indicate that the hotel must have wifi</value>
        [Display(Name = "Wifi")]
        public bool HasWifi { get; set; }

        /// <value>A boolean value to indicate that the hotel must have a parking</value>
        [Display(Name = "Parking")]
        public bool HasParking { get; set; }

        /// <value>A boolean value to indicate that the hotel must be a one star</value>
        [Display(Name = "1 star")]
        public bool Star1 { get; set; }

        /// <value>A boolean value to indicate that the hotel must be a two stars</value>
        [Display(Name = "2 stars")]
        public bool Star2 { get; set; }

        /// <value>A boolean value to indicate that the hotel must be a three stars</value>
        [Display(Name = "3 stars")]
        public bool Star3 { get; set; }

        /// <value>A boolean value to indicate that the hotel must be a four stars</value>
        [Display(Name = "4 stars")]
        public bool Star4 { get; set; }

        /// <value>A boolean value to indicate that the hotel must be a five stars</value>
        [Display(Name = "5 stars")]
        public bool Star5 { get; set; }

        /// <value>The maximum price of the rooms</value>
        [Range(0, 500, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int MaxPrice { get; set; } = 500;

        /// <value>A boolean value to indicate that the rooms must be simple rooms</value>
        [Display(Name = "Simple room")]
        public bool SimpleRoom { get; set; }

        /// <value>A boolean value to indicate that the rooms must be double rooms</value>
        [Display(Name = "Double room")]
        public bool DoubleRoom { get; set; }

        /// <value>A boolean value to indicate that the rooms must have a TV</value>
        [Display(Name = "TV")]
        public bool HasTV { get; set; }

        /// <value>A boolean value to indicate that the rooms must have a hair dryer</value>
        [Display(Name = "Hair dryer")]
        public bool HasHairDryer { get; set; }

        /// <value>A list of boolean values to indicate the stars that the hotel must have</value>
        public List<bool> Stars
        {
            get { return new List<bool>() { Star1, Star2, Star3, Star4, Star5 }; }
            set { }
        }
    }
}
