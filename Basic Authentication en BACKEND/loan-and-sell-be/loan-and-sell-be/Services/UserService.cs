using loan_and_sell_be.Models;

namespace loan_and_sell_be.Services
{
    public class UserService : IUserService
    {
        List<User> users = new List<User>(){
            new User(){ Email = "test1@mail.com", Password ="123" },
            new User(){ Email = "test2@mail.com", Password ="123" }
        };

        public bool IsUser(string email, string password) => 
            users.Where(x => x.Email == email && x.Password == password).Count() > 0;
    }
}
