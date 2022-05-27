using Knigochei.Models;

namespace Knigochei.Repository.CartRepo
{
    public interface ICartRepository
    {
        public IEnumerable<CartItem> GetCartItemsByCartId(int cartId);
        public Cart FindCartByUserId(int userId);
        public CartItem FindCartItemByCartIdAndBookId(int cartId, int bookId);
        public void CreateCart(int userId);
        public void AddBookToCart(int userId, int bookId, int amount);
        public void UpdateCartItem(CartItem cartItem);
        public void DeleteCartItem(CartItem cartItem);
        public bool IsUserHasCart(int userId);
        public bool ExistsCartItem(int userId, int bookId);

        public void DeleteCartItemsByCartId(int cartId);
        public void DeleteAllCartItemsByBookId(int bookId);



    }
}
