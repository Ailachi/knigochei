using Knigochei.Models;

namespace Knigochei.Services.AuthorService
{
    public interface IAuthorService
    {
        public List<Author> GetAllAuthors();
        public void AddAuthor(Author author);
        public void DeleteAuthorById(int authorId);
        public void GetAuthorById(int authorId);
    }
}
