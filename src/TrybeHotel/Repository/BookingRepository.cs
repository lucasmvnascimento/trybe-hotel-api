using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            var room = _context.Rooms.Find(booking.RoomId);
            if (room!.Capacity < booking.GuestQuant) throw new InvalidOperationException("Guest quantity over room capacity");
            var newBooking = new Booking
            {
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                UserId = _context.Users.Where(u => u.Email == email).Select(u => u.UserId).FirstOrDefault(),
            };
            _context.Bookings.Add(newBooking);
            _context.SaveChanges();
            return new BookingResponse
            {
                BookingId = newBooking.BookingId,
                CheckIn = newBooking.CheckIn,
                CheckOut = newBooking.CheckOut,
                GuestQuant = newBooking.GuestQuant,
                Room = new RoomDto
                {
                    RoomId = room.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = _context.Hotels.Where(h => h.HotelId == room.RoomId).Select(h => new HotelDto
                    {
                        HotelId = h.HotelId,
                        Name = h.Name,
                        Address = h.Address,
                        CityId = h.CityId,
                        CityName = _context.Cities.Where(c => c.CityId == h.CityId).First().Name,
                    }).First(),
                }
            };
        }

        public BookingResponse GetBooking(int bookingId, string email)
        {
            var booking = _context.Bookings.Find(bookingId);
            var bookingUser = _context.Users.Find(booking!.UserId);
            if (bookingUser!.Email != email) throw new UnauthorizedAccessException();
            return new BookingResponse
            {
                BookingId = booking!.BookingId,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                Room = _context.Rooms.Where(r => r.RoomId == booking.RoomId).Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Image = r.Image,
                    Hotel = _context.Hotels.Where(h => h.HotelId == r.HotelId).Select(h => new HotelDto
                    {
                        HotelId = h.HotelId,
                        Name = h.Name,
                        Address = h.Address,
                        CityId = h.CityId,
                        CityName = _context.Cities.Where(c => c.CityId == h.CityId).First().Name,
                    }).First(),
                }).First(),
            };
        }

        public Room GetRoomById(int RoomId)
        {
            throw new NotImplementedException();
        }

    }

}