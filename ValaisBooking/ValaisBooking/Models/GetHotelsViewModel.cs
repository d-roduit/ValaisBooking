using DataTransferObject;
using System.Collections.Generic;

namespace ValaisBooking.Models
{
    /// <summary>
    ///     The GetHotelsViewModel class wraps a SearchHotelsViewModel object <see cref="SearchHotelsViewModel"/>, and a list of hotels in a model to be able to display a list of hotels easily
    /// </summary>
    public class GetHotelsViewModel
    {
        /// <value>A SearchHotelsViewModel object that contains search information to find hotels</value>
        public SearchHotelsViewModel SearchHotelsViewModel  { get; set; }

        /// <value>A list of Hotel objects</value>
        public List<Hotel> Hotels { get; set; }

        /// <summary>
        ///     Constructor of the GetHotelsViewModel class
        /// </summary>
        public GetHotelsViewModel() { }

        /// <summary>
        ///     Constructor of the GetHotelsViewModel class
        /// </summary>
        /// <param name="searchHotelsViewModel">
        ///     A SearchHotelsViewModel object that contains search information to find hotels
        /// </param>
        /// <param name="hotels">
        ///     A list of Hotel objects
        /// </param>
        public GetHotelsViewModel(SearchHotelsViewModel searchHotelsViewModel, List<Hotel> hotels)
        {
            SearchHotelsViewModel = searchHotelsViewModel;
            Hotels = hotels;
        }
    }
}
