namespace DataTransferObject
{
    /// <summary>
    /// The Picture class represents a picture object
    /// </summary>
    public class Picture
    {
        /// <value>
        /// The picture id
        /// </value>
        public int IdPicture { get; }

        /// <value>
        /// The picture URL
        /// </value>
        public string URL { get; }

        /// <value>
        /// The room id
        /// </value>
        public int Room_IdRoom { get; }

        /// <summary>
        /// The Picture constructor
        /// </summary>
        /// <param name="idPicture">
        ///     The picture id
        /// </param>
        /// <param name="url">
        ///     The picture URL
        /// </param>
        /// <param name="room_IdRoom">
        ///     The room id
        /// </param>
        public Picture(int idPicture, string url, int room_IdRoom)
        {
            IdPicture = idPicture;
            URL = url;
            Room_IdRoom = room_IdRoom;
        }

        /// <summary>
        ///     Display the picture object as a string
        ///     Override of the parent ToString() method
        /// </summary>
        /// <returns>
        ///     The picture object as a string
        /// </returns>
        public override string ToString()
        {
            return $"Picture {{ {nameof(IdPicture)}: {IdPicture}, {nameof(URL)}: {URL.Substring(0, 30)}..., {nameof(Room_IdRoom)}: {Room_IdRoom} }}";
        }
    }
}
