using Knigochei.Models;
using Knigochei.Repository.AuthorRepo;
using Knigochei.Services.GenreService;
using Knigochei.UnitOfWorkDapper;

namespace Knigochei.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private IUnitOfWork _uow;

        public AuthorService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void AddAuthor(Author author)
        {
            IAuthorRepository repo = _uow.AuthorRepository;
            repo.Add(author);
            _uow.Commit();
        }

        public void DeleteAuthorById(int authorId)
        {
            IAuthorRepository repo = _uow.AuthorRepository;
            repo.Delete(authorId);
            _uow.Commit();


        }

        public List<Author> GetAllAuthors()
        {
            IAuthorRepository repo = _uow.AuthorRepository;
            List<Author> authors = repo.All().Where(author => !author.IsDeleted).ToList();

            return authors;
        }

        public Author GetAuthorById(int authorId)
        {
            IAuthorRepository repo = _uow.AuthorRepository;
            Author author = repo.Find(authorId);

            if (author is null)
            {
                author = new Author();
                author.FirstName = "Неизвестный";
                author.LastName = "Автор";
            }

            return author;
        }

        public void FilterAuthorsByFirstName(ref List<Author> authors, string firstName)
        {

            authors = String.IsNullOrEmpty(firstName)
                ? authors
                : authors.Where(author => author.FirstName.ToLower().Contains(firstName.Trim().ToLower())).ToList();
        }

        public void FilterAuthorsByLastName(ref List<Author> authors, string lastName)
        {
            authors = String.IsNullOrEmpty(lastName)
                ? authors
                : authors.Where(author => author.LastName.ToLower().Contains(lastName.Trim().ToLower())).ToList();
        }

        public void SortAuthorsByFirstNameDesc(ref List<Author> authors, bool isSortByDesc)
        {
            authors = isSortByDesc ? authors.OrderByDescending(author => author.FirstName).ToList() : authors.OrderBy(author => author.FirstName).ToList();
        }

        public List<Author> GetFilteredAuthorsByGenre(int genreId)
        {
            IAuthorRepository repo = _uow.AuthorRepository;


            List<Author> authors = genreId == 0
                ? GetAllAuthors().Where(author => !author.IsDeleted).ToList()
                : repo.GetAuthorsByGenreId(genreId).Where(author => !author.IsDeleted).ToList();

            return authors;
        }

        public Author FindAuthorByBook(int bookId)
        {
            IAuthorRepository repo = _uow.AuthorRepository;

            Author author = repo.FindAuthorByBookId(bookId);

            if(author is null)
            {
                author = new Author();
                author.FirstName = "Неизвестный";
                author.LastName = "Автор";
            }

            return author;

        }

        public List<Gender> GetGenders()
        {
            IAuthorRepository repo = _uow.AuthorRepository;
            List<Gender> genders = repo.GetAllGenders().ToList();

            return genders ?? new List<Gender>();
        }
    }
}
