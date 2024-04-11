using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    //
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            var room = _context.Rooms.Find(booking.RoomId);
            var hotel = _context.Hotels.Find(room.HotelId);
            var city = _context.Cities.FirstOrDefault(c => c.CityId == hotel.CityId);
            var bookingEntity = new Booking
            {
                GuestQuant = booking.GuestQuant,
                RoomId = booking.RoomId,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                UserId = user.UserId
            };
            var newBooking = _context.Bookings.Add(bookingEntity);
            _context.SaveChanges();
            return new BookingResponse
            {
                BookingId = newBooking.Entity.BookingId,
                GuestQuant = newBooking.Entity.GuestQuant,
                Room = new RoomDto
                {
                    RoomId = room.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = hotel.HotelId,
                        Name = hotel.Name,
                        Address = hotel.Address,
                        CityId = city.CityId,
                        CityName = city.Name,
                        State = city.State
                    }
                },
                CheckIn = newBooking.Entity.CheckIn,
                CheckOut = newBooking.Entity.CheckOut
            };
        }

        public BookingResponse GetBooking(int bookingId, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            var booking = _context.Bookings.Find(bookingId);
            var room = _context.Rooms.Find(booking.RoomId);
            var hotel = _context.Hotels.Find(room.HotelId);
            var city = _context.Cities.FirstOrDefault(c => c.CityId == hotel.CityId);
            return new BookingResponse
            {
                BookingId = booking.BookingId,
                GuestQuant = booking.GuestQuant,
                Room = new RoomDto
                {
                    RoomId = room.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = hotel.HotelId,
                        Name = hotel.Name,
                        Address = hotel.Address,
                        CityId = city.CityId,
                        CityName = city.Name,
                        State = city.State
                    }
                },
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut
            };
        }

        public Room GetRoomById(int RoomId)
        {
            throw new NotImplementedException();
        }

    }

}