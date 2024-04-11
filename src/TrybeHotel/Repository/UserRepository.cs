using TrybeHotel.Models;
using TrybeHotel.Dto;

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

        public UserDto? Login(LoginDto login)
        {
            UserDto? toReturn = (from usr in _context.Users
                                where usr.Email == login.Email && usr.Password == login.Password
                                select new UserDto
                                {
                                    UserId = usr.UserId,
                                    Name = usr.Name,
                                    Email = usr.Email,
                                    UserType = usr.UserType
                                }).FirstOrDefault();
            return toReturn;
        }
        public UserDto Add(UserDtoInsert user)
        {
            _context.Users.Add(new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = "client"
            });
            _context.SaveChanges();
            UserDto toReturn = (from usr in _context.Users
                               where usr.Email == user.Email
                               select new UserDto
                               {
                                   UserId = usr.UserId,
                                   Name = usr.Name,
                                   Email = usr.Email,
                                   UserType = usr.UserType
                               }).First();
        return toReturn;
        }

        public UserDto? GetUserByEmail(string userEmail)
        {
            UserDto? toReturn = (from usr in _context.Users
                                where usr.Email == userEmail
                                select new UserDto
                                {
                                    UserId = usr.UserId,
                                    Name = usr.Name,
                                    Email = usr.Email,
                                    UserType = usr.UserType
                                }).FirstOrDefault();
            return toReturn;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            List<UserDto> toReturn = (from usr in _context.Users
                                    select new UserDto
                                    {
                                        UserId = usr.UserId,
                                        Name = usr.Name,
                                        Email = usr.Email,
                                        UserType = usr.UserType
                                    }).ToList();
            return toReturn;
        }

    }
}