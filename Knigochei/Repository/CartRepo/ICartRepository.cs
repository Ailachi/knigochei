using Knigochei.Models;

namespace Knigochei.Repository.CartRepo
{
    public interface ICartRepository
    {
        public Cart FindCartByUserId(int userId);
        public CartItem FindCartItemByCartId(int cartId);
        public void CreateCart(int userId);
        public void AddBookToCart(int userId, int bookId, int amount);
        public void UpdateCartItem(CartItem cartItem);
        public bool IsUserHasCart(int userId);
        public bool ExistsCartItem(int userId, int bookId);

        

    }
}
