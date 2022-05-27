using Knigochei.Repository.AuthorRepo;
using Knigochei.Repository.BookRepo;
using Knigochei.Repository.CartRepo;
using Knigochei.Repository.GenreRepo;
using Knigochei.Repository.OrderRepo;
using Knigochei.Repository.UserRepo;

namespace Knigochei.UnitOfWorkDapper
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; set; }
        IAuthorRepository AuthorRepository { get; set; }
        IGenreRepository GenreRepository{ get; set; }
        IUserRepository UserRepository { get; set; }
        ICartRepository CartRepository{ get; set; }
        IOrderRepository OrderRepository{ get; set; }
        void Commit();
    }
}
