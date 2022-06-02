using DataAccessLayer;
using DataTransferObject;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    /// <summary>
    /// The PictureManager class for managing the pictures
    /// </summary>
    public class PictureManager : IPictureManager
    {
        /// <value>
        /// The Picture Interface of Data Access Layer
        /// </value>
        private IPictureDB PictureDB { get; }

        /// <summary>
        /// The PictureManager constructor
        /// </summary>
        /// <param name="pictureDB">
        ///     The Picture Interface of Data Acces Layer
        /// </param>
        public PictureManager(IPictureDB pictureDB)
        {
            PictureDB = pictureDB;
        }

        /// <summary>
        ///     Retrieves the pictures found for the given room id
        /// </summary>
        /// <param name="idRoom">
        ///     The room id
        /// </param>
        /// <returns>
        ///     A list of pictures
        /// </returns>
        public List<Picture> GetPicturesByRoom(int idRoom)
        {
            List<Picture> pictures = null;

            try
            {
                pictures = PictureDB.GetPicturesByRoom(idRoom);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return pictures;
        }

    }
}
