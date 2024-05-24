using Web_Api_Testing_Migration.Data;
using Web_Api_Testing_Migration.Models;
using Web_Api_Testing_Migration.Repositories.Interfaces;

namespace Web_Api_Testing_Migration.Repositories.Implements
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;

        public UsersRepository(DataContext context)
        {
            _context = context;
        }
        public bool isExist(string username)
        {
            return _context.users.Any(acc => acc.user_name == username);
        }

        public bool registerUser(users users)
        {
            _context.Add(users);
            return saveChanges();
        }

        public bool saveChanges()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool signInUser(string username, string password)
        {
            return _context.users.Any(acc => acc.user_name == username && acc.password == password);
        }

        public bool updateUser(users users)
        {
            _context.Update(users);
            return saveChanges();
        }


    }
}
