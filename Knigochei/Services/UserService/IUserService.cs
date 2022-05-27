using Knigochei.Forms;
using Knigochei.Models;

namespace Knigochei.Services.UserService
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public User GetUserByEmailAndPassword(string email, string password);
        public User GetUserById(int id);
        public string GetUserRole(int roleId);
        public bool ExistsByEmail(string email);
        public void CreateNewUser(RegistrationFormModel model);
        public void EditUser(User user);




    }
}
