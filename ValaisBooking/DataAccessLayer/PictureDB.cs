using DataTransferObject;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// The PictureDB class
    /// Contains all methods for retrieving pictures data from the database
    /// </summary>
    public class PictureDB : IPictureDB
    {
        /// <value>
        /// The Configuartion Interface
        /// </value>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The PictureDB constructor
        /// </summary>
        /// <param name="configuration">
        ///     The Configuration Interface
        /// </param>
        public PictureDB(IConfiguration configuration)
        {
            this.configuration = configuration;
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
                using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ValaisBookingDB"));
                string query = "SELECT * FROM Picture WHERE Room_IdRoom = @idRoom";
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                sqlCommand.Parameters.AddWithValue("@idRoom", idRoom);

                connection.Open();

                using SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    if (pictures == null)
                    {
                        pictures = new List<Picture>();
                    }

                    int idPicture = (int)dataReader["IdPicture"];
                    string url = (string)dataReader["Url"];

                    Picture picture = new Picture(idPicture, url, idRoom);

                    pictures.Add(picture);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return pictures;
        }

    }
}
