using Web_Api_Testing_Migration.Models;

namespace Web_Api_Testing_Migration.Services.Interfaces
{
    public interface IUsersService
    {
        void signInUser(string username, string password);
        void registerUser(users users);

        void updateUser(users users, int username);
    }
}
