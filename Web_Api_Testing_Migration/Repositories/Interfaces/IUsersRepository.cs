using Web_Api_Testing_Migration.Models;

namespace Web_Api_Testing_Migration.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        bool signInUser(string username, string password);
        bool registerUser(users users);
        bool updateUser(users users);
        bool isExist(string username);

        bool saveChanges();
    }
}
