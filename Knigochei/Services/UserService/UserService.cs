using Knigochei.Forms;
using Knigochei.Models;
using Knigochei.Repository.UserRepo;
using Knigochei.UnitOfWorkDapper;

namespace Knigochei.Services.UserService
{
    public class UserService : IUserService
    {

        private IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            IUserRepository repo = _uow.UserRepository;
            User user = repo.FindByEmailAndPassword(email, password);

            return user;
        }

        public string GetUserRole(int roleId)
        {
            switch (roleId)
            {
                case (int)Roles.Admin: return "Admin";
                case (int)Roles.Customer: return "Customer";

                default: return "Customer";
            }
        }


        public void CreateNewUser(RegistrationFormModel model)
        {
            User user = GetUserFromRegistrationFormModel(model);
            IUserRepository repo = _uow.UserRepository;
            repo.Add(user);
            _uow.Commit();
        }
        public bool ExistsByEmail(string email)
        {
            IUserRepository repo = _uow.UserRepository;
            User user = repo.FindByEmail(email);

            return user is null ? false : true;
        }
        private User GetUserFromRegistrationFormModel(RegistrationFormModel model)
        {
            User user = new User();
            user.Email = model.Email;
            user.UserPassword = model.Password;
            user.FirstName = model.FirsName;
            user.LastName = model.LastName;
            user.GenderId = model.GenderId;
            user.RoleId = (int)Roles.Customer;

            return user;
        }

		public User GetUserById(int id)
		{
            IUserRepository repo = _uow.UserRepository;
            User user = repo.Find(id);

            return user ?? new User();
        }

		public void EditUser(User user)
		{
            IUserRepository repo = _uow.UserRepository;
            repo.Update(user);
            _uow.Commit();

        }
    }
}
