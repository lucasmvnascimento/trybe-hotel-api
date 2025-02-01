using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<RoomDto> GetRooms(int HotelId) {
            var query = _context.Rooms.Where(r => r.HotelId == HotelId)
                .Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Image = r.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = r.Hotel!.HotelId,
                        Name = r.Hotel.Name,
                        Address = r.Hotel.Address,
                        CityId = r.Hotel.CityId,
                        CityName = r.Hotel.City!.Name
                    }
                });
            return query.ToList();
        }

        public RoomDto AddRoom(Room room) {
            _context.Rooms.Add(room);
            _context.SaveChanges();
            var newRoom = _context.Rooms
                .Where(r => r.RoomId == room.RoomId)
                .Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Image = r.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = r.Hotel!.HotelId,
                        Name = r.Hotel.Name,
                        Address = r.Hotel.Address,
                        CityId = r.Hotel.CityId,
                        CityName = r.Hotel.City!.Name
                    }
                }).First();
            return newRoom;
        }

        public void DeleteRoom(int RoomId) {
            var room = _context.Rooms.Find(RoomId);
            if (room != null) {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
        }
    }
}