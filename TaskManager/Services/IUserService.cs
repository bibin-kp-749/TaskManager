using TaskManager.Model;

namespace TaskManager.Services
{
    //Interface for managing User
    public interface IUserService
    {
        public IEnumerable<User> RegisterUser(User user);
        public User Login(Login user);
    }
}
