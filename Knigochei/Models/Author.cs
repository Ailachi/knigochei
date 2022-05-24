using System.ComponentModel.DataAnnotations;

namespace Knigochei.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } } 
        public string AvatarImagePath { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
    }
}
