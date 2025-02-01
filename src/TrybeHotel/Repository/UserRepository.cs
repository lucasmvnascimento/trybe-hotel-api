using TrybeHotel.Models;
using TrybeHotel.Dto;
using System.Data;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == login.Email)
                ?? throw new InvalidDataException("Incorrect e-mail or password");
            if (user!.Password != login.Password) throw new InvalidDataException("Incorrect e-mail or password");
            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                UserType = user.UserType
            };
        }
        public UserDto Add(UserDtoInsert user)
        {
            var userFound = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            Console.WriteLine(userFound);
            if (userFound != null) throw new InvalidConstraintException("User email already exists");
            _context.Users.Add(new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = "client"
            });
            _context.SaveChanges();
            var newUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            return new UserDto
            {
                UserId = newUser!.UserId,
                Name = newUser.Name,
                Email = newUser.Email,
                UserType = newUser.UserType
            };
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var query = _context.Users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                UserType = u.UserType,
            });
            return query.ToList();
        }

    }
}