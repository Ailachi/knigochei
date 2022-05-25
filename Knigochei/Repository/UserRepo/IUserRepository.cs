using Knigochei.Models;

namespace Knigochei.Repository.UserRepo
{
    public interface IUserRepository
    {
        void Add(User user);
        IEnumerable<User> All();
        void Delete(int id);
        void Delete(User user);
        User Find(int id);
        User FindByFirstName(string firstName);
        User FindByEmail(string email);
        User FindByEmailAndPassword(string email, string password);
        void Update(User user);
    }
}
