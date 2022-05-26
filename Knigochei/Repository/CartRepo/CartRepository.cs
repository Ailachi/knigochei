using System.Data;
using Dapper;

namespace Knigochei.Repository.CartRepo
{
    internal class CartRepository : RepositoryBase, ICartRepository
    {
        public CartRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void CreateCart(int userId)
        {
            //SELECT SCOPE_IDENTITY();
            int cartId = Connection.ExecuteScalar<int>(
                sql: "INSERT INTO Cart(UserId) VALUES(@userId); " +
                     "SELECT SCOPE_IDENTITY();",
                param: new { userId },
                transaction: Transaction
            );
        }

        public bool IsUserHasCart(int userId)
        {
            return Connection.Query<bool>(
                sql: "SELECT COUNT(*) FROM Cart WHERE UserId = @userId;",
                param: new { userId },
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
}
