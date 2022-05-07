using Knigochei.Models;
using System.Data;

namespace Knigochei.Repository.RoleRepo
{
    internal class RoleRepository : RepositoryBase, IRoleRepository
    {
        public RoleRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Add(Role genre)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> All()
        {
            throw new NotImplementedException();
        }
    }
}
