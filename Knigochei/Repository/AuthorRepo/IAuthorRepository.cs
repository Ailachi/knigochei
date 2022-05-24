using Knigochei.Models;

namespace Knigochei.Repository.AuthorRepo
{
    public interface IAuthorRepository
    {
        void Add(Author author);
        IEnumerable<Author> All();
        void Delete(int id);
        void Delete(Author author);
        Author Find(int id);
        void Update(Author author);
    }
}
