using BusinessLogicLayer;
using DataAccessLayer;
using DataTransferObject;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    /// <summary>
    /// Console app to test the DAL & BLL.
    /// </summary>
    class Program
    {
       public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        

        static void Main(string[] args)
        {

            IHotelDB hotelDB = new HotelDB(Configuration);
            IRoomDB roomDB = new RoomDB(Configuration);
            IReservationDB reservationDB = new ReservationDB(Configuration);
            IPictureDB pictureDB = new PictureDB(Configuration);

            HotelManager hotelManager = new HotelManager(hotelDB);
            RoomManager roomManager = new RoomManager(roomDB);
            ReservationManager reservationManager = new ReservationManager(reservationDB);
            PictureManager pictureManager = new PictureManager(pictureDB);


            /*-----------------------------------------------------------------
                                        HOTEL TESTS
            -----------------------------------------------------------------*/
            List<Hotel> hotels = null;


            Console.WriteLine("\n/*-----------------------------------------------------------------");
            Console.WriteLine("\t\t\tHOTEL TESTS");
            Console.WriteLine("-----------------------------------------------------------------*/");


            // GetAllHotels()
            Console.WriteLine("\n\n// GetAllHotels()\n");

            hotels = hotelManager.GetAllHotels();

            Console.WriteLine($"{hotels.Count} hotels trouvés :");

            for (int i = 0; i < hotels.Count; i++)
            {
                Console.WriteLine($"{i}) {hotels[i]}");
            }


            // GetHotel()
            Console.WriteLine("\n\n// GetHotel()\n");

            int idHotel = 2;

            Hotel hotel = hotelManager.GetHotel(idHotel);

            Console.WriteLine($"Hotel id {idHotel} récupéré :");
            Console.WriteLine($"{hotel}");


            // GetHotels()
            Console.WriteLine("\n\n// GetHotels()\n");

            string destination = "Martigny";
            DateTime checkIn = new DateTime(2020, 11, 06);
            DateTime checkOut = new DateTime(2020, 11, 09);
            bool hasWifi = true;
            bool hasParking = false;
            List<bool> categories = new List<bool>() { false, false, false, false, false };
            bool hasTV = true;
            bool hasHairDryer = false;
            int minPrice = 0;
            int maxPrice = 500;
            bool simpleRoom = true;
            bool doubleRoom = true;

            hotels = hotelManager.GetHotels(destination, checkIn, checkOut, hasWifi, hasParking, categories, hasTV, hasHairDryer, minPrice, maxPrice, simpleRoom, doubleRoom);
            Console.WriteLine($"{hotels.Count} hotel(s) trouvé(s) :");
            for (int i = 0; i < hotels.Count; i++)
            {
                Console.WriteLine($"{i}) {hotels[i]}");
            }


            /*-----------------------------------------------------------------
                                        ROOM TESTS
            -----------------------------------------------------------------*/
            List<Room> rooms = null;

            Console.WriteLine("\n\n/*-----------------------------------------------------------------");
            Console.WriteLine("\t\t\tROOM TESTS");
            Console.WriteLine("-----------------------------------------------------------------*/");


            // GetRoomsByHotel()
            Console.WriteLine("\n\n// GetRoomsByHotel()\n");

            idHotel = 2;

            rooms = roomManager.GetRoomsByHotel(idHotel);

            Console.WriteLine($"{rooms.Count} chambre(s) trouvée(s) pour l'hotel id {idHotel} :");

            for (int i = 0; i < rooms.Count; i++)
            {
                Console.WriteLine($"{i}) {rooms[i]}");
            }


            // GetAvailableRoomsByHotel()
            Console.WriteLine("\n\n\n// GetAvailableRoomsByHotel()\n");

            idHotel = 1;
            checkIn = new DateTime(2020, 11, 08);
            checkOut = new DateTime(2020, 11, 15);

            rooms = roomManager.GetAvailableRoomsByHotel(idHotel, checkIn, checkOut, hasTV, hasHairDryer, minPrice, maxPrice, simpleRoom, doubleRoom);

            Console.WriteLine($"{rooms.Count} chambre(s) trouvée(s) pour l'hotel id {idHotel} pour la période {checkIn} - {checkOut} :");

            for (int i = 0; i < rooms.Count; i++)
            {
                Console.WriteLine($"{i}) {rooms[i]}");
            }


            // GetRoom()
            Console.WriteLine("\n\n// GetRoom()\n");

            int idRoom = 4;

            Room room = roomManager.GetRoom(idRoom);

            Console.WriteLine($"Room id {idRoom} récupérée :");
            Console.WriteLine(room);


            /*-----------------------------------------------------------------
                                    RESERVATION TESTS
            -----------------------------------------------------------------*/
            Console.WriteLine("\n\n/*-----------------------------------------------------------------");
            Console.WriteLine("\t\t\tRESERVATION TESTS");
            Console.WriteLine("-----------------------------------------------------------------*/");


            // AddReservation()
            Console.WriteLine("\n\n// AddReservation()\n");

            int reservationNumber = 45000;
            string firstname = "Michel";
            string lastname = "Dorsaz";
            checkIn = DateTime.Now;
            checkOut = DateTime.Now.AddDays(3);
            idRoom = 2;

            int nbRowsAffected = reservationManager.AddReservation(reservationNumber, firstname, lastname, checkIn, checkOut, idRoom);

            Console.WriteLine($"Nombre de lignes affectées par l'insertion d'une réservation : {nbRowsAffected}");


            // GetReservation()
            Console.WriteLine("\n\n// GetReservation()\n");


            List<Reservation> reservations = reservationManager.GetReservations(reservationNumber, firstname, lastname);

            Console.WriteLine($"Reservation No {reservationNumber} récupérée :");
            foreach (Reservation reservation in reservations)
            {
                Console.WriteLine(reservation);
            }


            /*-----------------------------------------------------------------
                                    PICTURE TESTS
            -----------------------------------------------------------------*/
            List<Picture> pictures = null;

            Console.WriteLine("\n\n/*-----------------------------------------------------------------");
            Console.WriteLine("\t\t\tPICTURE TESTS");
            Console.WriteLine("-----------------------------------------------------------------*/");


            // GetPicturesByRoom()
            Console.WriteLine("\n\n// GetPicturesByRoom()\n");

            idRoom = 6;

            pictures = pictureManager.GetPicturesByRoom(idRoom);

            Console.WriteLine($"{pictures.Count} photo(s) trouvée(s) :");

            for (int i = 0; i < pictures.Count; i++)
            {
                Console.WriteLine($"{i}) {pictures[i]}");
            }

        }
    }
}
