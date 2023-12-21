using UserService.Models;

namespace UserService.Data
{
    public interface IUserRepo
    {
        bool SaveChanges();
        void CreateUser(User user);
        User GetUser(int id);
        User GetUserByEmail(string email);
        void DeleteUser(int id);
        bool AuthenticateUser(string email, string password);
        bool CheckEmailAvailability(string email);
        IEnumerable<User> GetUsers();
    }
}
