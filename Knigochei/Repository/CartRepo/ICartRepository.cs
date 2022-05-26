namespace Knigochei.Repository.CartRepo
{
    public interface ICartRepository
    {
        public void CreateCart(int userId);
        public bool IsUserHasCart(int userId);
        

    }
}
