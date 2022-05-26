using Knigochei.Models;
using Knigochei.Repository.CartRepo;
using Knigochei.UnitOfWorkDapper;

namespace Knigochei.Services.CartService
{
    public class CartService : ICartService
    {
        private IUnitOfWork _uow;

        public CartService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void AddBookToCart(int userId, int bookId, int amount=1)
        {
            ICartRepository repo = _uow.CartRepository;
            Cart cart = repo.FindCartByUserId(userId);

            repo.AddBookToCart(cart.Id, bookId, amount);
            _uow.Commit();
        }

        public void CreateUserCartIfNotExists(int userId)
        {
            if (!IsUserHasCart(userId))
            {
                CreateUserCart(userId);
            }
        }

        public void CreateUserCart(int userId)
        {
            ICartRepository repo = _uow.CartRepository;
            repo.CreateCart(userId);
            _uow.Commit();
        }

        public bool IsUserHasCart(int userId)
        {
            ICartRepository repo = _uow.CartRepository;
            bool hasCart = repo.IsUserHasCart(userId);

            return hasCart;
        }

        public void UpdateUserCartItemAmountOrIncrement(int userId, int bookId, int amount=0)
        {
            ICartRepository repo = _uow.CartRepository;
            Cart cart = repo.FindCartByUserId(userId);

            CartItem cartItem = repo.FindCartItemByCartId(cart.Id);

            cartItem.Amount = amount == 0
                ? cartItem.Amount + 1
                : amount;

            repo.UpdateCartItem(cartItem);
            _uow.Commit();
        }


        public bool ExistsCartItem(int userId, int bookId)
        {
            ICartRepository repo = _uow.CartRepository;
            bool existsCartItem = repo.ExistsCartItem(userId, bookId);

            return existsCartItem;
        }
    }
}
