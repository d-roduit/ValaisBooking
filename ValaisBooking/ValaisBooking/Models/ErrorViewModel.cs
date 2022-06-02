using System;

namespace ValaisBooking.Models
{
    /// <summary>
    ///     The ErrorViewModel class wraps an id in a model to be able to easily display an Error view for whatever error
    /// </summary>
    public class ErrorViewModel
    {
        /// <value>A request id</value>
        public string RequestId { get; set; }

        /// <summary>
        ///     Indicate if the request id can be shown
        /// </summary>
        /// <returns>
        ///     A boolean value that indicate if the request id is null or empty
        /// </returns>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
