using System.Numerics;
using TaskManager.Model;

namespace TaskManager.Services
{
    //Inherited Class of IUserService Interface
    public class UserService:IUserService
    {
        public static List<User> users = new List<User>
        {
            new User{UserId=1,UserName="Bibin",Password="12345",Role=Role.Admin},
            new User{UserId=2,UserName="Vipin",Password="12345",Role=Role.User},
        };
        public IEnumerable<User> RegisterUser(User user)
        {
            users.Add(new User { UserId = users.Count + 1,UserName=user.UserName,Password=user.Password,Role=user.Role }) ;
            return users ;
        }
        public User Login(Login user)
        {
            var result=users.FirstOrDefault(s=>s.UserName==user.UserName && s.Password==user.Password);
            return result;
        }
    }
}
