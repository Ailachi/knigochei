using System.Data;
using Dapper;
using Knigochei.Models;

namespace Knigochei.Repository.CartRepo
{
    internal class CartRepository : RepositoryBase, ICartRepository
    {
        public CartRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void AddBookToCart(int cartId, int bookId, int amount)
        {
            int cartItemId = Connection.ExecuteScalar<int>(
                sql: "INSERT INTO CartItem(CartId, BookId, Amount) VALUES(@cartId, @bookId, @amount); " +
                     "SELECT SCOPE_IDENTITY();",
                param: new { cartId, bookId, amount },
                transaction: Transaction
            );
        }

       
        public void CreateCart(int userId)
        {
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
        public bool ExistsCartItem(int userId, int bookId)
        {
            return Connection.Query<bool>(
                sql: "SELECT COUNT(*) FROM CartItem ci " +
                     "INNER JOIN Cart c on c.Id = ci.CartId " +
                     "WHERE c.UserId = @userId AND ci.BookId = @bookId",
                param: new { userId, bookId },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public Cart FindCartByUserId(int userId)
        {
            return Connection.Query<Cart>(
                sql: "SELECT * FROM Cart WHERE UserId = @userId",
                param: new { userId },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            int cartItemId = Connection.ExecuteScalar<int>(
                sql: "UPDATE CartItem " +
                     "SET CartId = @cartId, " +
                     "BookId = @bookId, " +
                     "Amount = @amount " +
                     "WHERE Id = @id",
                param: 
                new 
                { @cartId = cartItem.CartId, 
                    @bookId = cartItem.BookId, 
                    @amount = cartItem.Amount, 
                    @id = cartItem.Id
                },
                transaction: Transaction
            );
        }

        public CartItem FindCartItemByCartId(int cartId)
        {
            return Connection.Query<CartItem>(
                sql: "SELECT * FROM CartItem WHERE CartId = @cartId",
                param: new { cartId },
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
}
