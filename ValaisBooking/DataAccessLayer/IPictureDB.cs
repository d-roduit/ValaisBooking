using DataTransferObject;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// <summary>
    /// The PictureDB Interface for accessing pictures data from database
    /// </summary>
    public interface IPictureDB
    {
        /// <summary>
        ///     Retrieves the pictures found for the given room id
        /// </summary>
        /// <param name="idRoom">
        ///     The room id
        /// </param>
        /// <returns>
        ///     A list of pictures
        /// </returns>
        List<Picture> GetPicturesByRoom(int idRoom);
    }
}