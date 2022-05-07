using Knigochei.Models;

namespace Knigochei.Repository.RoleRepo
{
    public interface IRoleRepository
    {
        void Add(Role genre);
        IEnumerable<Role> All();
        
    }
}
