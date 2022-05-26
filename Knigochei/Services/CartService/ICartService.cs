namespace Knigochei.Services.CartService
{
    public interface ICartService
    {
        public void CreateUserCart(int userId);
        public bool IsUserHasCart(int userId);

    }
}
