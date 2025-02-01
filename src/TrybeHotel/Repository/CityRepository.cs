using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<CityDto> GetCities()
        {
            var query = _context.Cities.Select(c => new CityDto
            {
                CityId = c.CityId,
                Name = c.Name,
                State = c.State
            });
            return query.ToList();
        }

        public CityDto AddCity(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
            var newCity = _context.Cities.Where(c => c.CityId == city.CityId).Select(c => new CityDto
            {
                CityId = c.CityId,
                Name = c.Name,
                State = c.State
            }).First();
            return newCity;
        }

        public CityDto UpdateCity(City city)
        {
            var cityFound = _context.Cities.Find(city.CityId);
            if (cityFound is null) throw new InvalidDataException("City not found");
            cityFound.Name = city.Name;
            cityFound.State = city.State;
            _context.SaveChanges();
            return new CityDto
            {
                CityId = cityFound.CityId,
                Name = cityFound.Name,
                State = cityFound.State,
            };
        }

    }
}