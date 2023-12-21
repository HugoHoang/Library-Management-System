using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly UserDbContext _context;

        public UserRepo(UserDbContext context)
        {
            _context = context;
        }


        public bool CheckEmailAvailability(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public async void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var existingUser = await _context.Users.Where(u => u.Email == user.Email).FirstOrDefaultAsync();

            if (existingUser == null)
            {
                throw new Exception("User already exists");
            }

            _context.Users.Add(user);
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new KeyNotFoundException("A user with this email was not found.");
            }

            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if(user == null)
            {
                throw new KeyNotFoundException("A user with this email was not found.");
            }

            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        bool IUserRepo.AuthenticateUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        bool IUserRepo.CheckEmailAvailability(string email)
        {
            throw new NotImplementedException();
        }
    }
}
