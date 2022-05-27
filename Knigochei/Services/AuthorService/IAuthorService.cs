using Knigochei.Models;

namespace Knigochei.Services.AuthorService
{
    public interface IAuthorService
    {
        public List<Author> GetAllAuthors();
        public void AddAuthor(Author author);
        public void DeleteAuthorById(int authorId);
        public Author GetAuthorById(int authorId);
        public void FilterAuthorsByFirstName(ref List<Author> authors, string firstName);
        public void FilterAuthorsByLastName(ref List<Author> authors, string lastName);
        public void SortAuthorsByFirstNameDesc(ref List<Author> authors, bool isSortByDesc);
        public List<Author> GetFilteredAuthorsByGenre(int genreId);
        public Author FindAuthorByBook(int bookId);

    }
}
