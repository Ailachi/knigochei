using Knigochei.Models;

namespace Knigochei.Repository.AuthorRepo
{
    public interface IAuthorRepository
    {
        void Add(Author author);
        IEnumerable<Author> All();
        IEnumerable<Author> GetAuthorsByGenreId(int genreId);
        void Delete(int id);
        void Delete(Author author);
        Author Find(int id);
        Author FindAuthorByBookId(int bookId);
        void Update(Author author);

        IEnumerable<Gender> GetAllGenders();
    }
}
