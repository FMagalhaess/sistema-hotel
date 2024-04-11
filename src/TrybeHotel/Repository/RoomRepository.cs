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

        // 6. Desenvolva o endpoint GET /room/:hotelId
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            List<RoomDto> toReturn = (from room in _context.Rooms
                            join hotel in _context.Hotels
                            on room.HotelId equals hotel.HotelId
                            join city in _context.Cities
                            on hotel.CityId equals city.CityId
                            where room.HotelId == HotelId
                            select new RoomDto {
                                RoomId = room.RoomId,
                                Name = room.Name,
                                Capacity = room.Capacity,
                                Image = room.Image,
                                Hotel = new HotelDto {
                                    HotelId = hotel.HotelId,
                                    Name = hotel.Name,
                                    CityId = hotel.CityId,
                                    CityName = city.Name,
                                    Address = hotel.Address,
                                    State = city.State
                                }
                            }).ToList();
            return toReturn;
        }

        // 7. Desenvolva o endpoint POST /room
        public RoomDto AddRoom(Room room) {
            _context.Rooms.Add(room);
            _context.SaveChanges();
            RoomDto toReturn = (from r in _context.Rooms
                                join h in _context.Hotels
                                on r.HotelId equals h.HotelId
                                join c in _context.Cities
                                on h.CityId equals c.CityId
                                where r.RoomId == room.RoomId
                                select new RoomDto {
                                    RoomId = r.RoomId,
                                    Name = r.Name,
                                    Capacity = r.Capacity,
                                    Image = r.Image,
                                    Hotel = new HotelDto {
                                        HotelId = h.HotelId,
                                        Name = h.Name,
                                        CityId = h.CityId,
                                        CityName = c.Name,
                                        Address = h.Address,
                                        State = c.State
                                    }
                                }).FirstOrDefault();
            return toReturn;
        }

        // 8. Desenvolva o endpoint DELETE /room/:roomId
        public void DeleteRoom(int RoomId)
        {
            _context.Rooms.Remove(_context.Rooms.Where(r => r.RoomId == RoomId).First());
            _context.SaveChanges();
        }
    }
}