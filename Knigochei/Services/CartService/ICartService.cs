namespace Knigochei.Services.CartService
{
    public interface ICartService
    {
        public void CreateUserCart(int userId);
        public void AddBookToCart(int userId, int bookId, int amount=1);
        public void CreateUserCartIfNotExists(int userId);
        public void UpdateUserCartItemAmountOrIncrement(int userId, int bookId, int amount=0);
        public bool IsUserHasCart(int userId);
        public bool ExistsCartItem(int userId, int bookId);


        


    }
}
