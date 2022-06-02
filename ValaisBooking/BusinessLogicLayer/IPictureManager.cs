using DataTransferObject;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    /// <summary>
    /// The PictureManager Interface for managing pictures
    /// </summary>
    public interface IPictureManager
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