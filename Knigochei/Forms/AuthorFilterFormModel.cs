using System.ComponentModel.DataAnnotations;

namespace Knigochei.Forms
{
    public class AuthorFilterFormModel
    {
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        public string LastName { get; set; }
        public int GenreId { get; set; } = 0;
        public bool OrderByFirstNameDesc { get; set; } = false;
    }
}
