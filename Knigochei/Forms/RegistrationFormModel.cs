using System.ComponentModel.DataAnnotations;
using Knigochei.Models;

namespace Knigochei.Forms
{
    public class RegistrationFormModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "The password should contain minimum 8 charachters!")]
        public string Password { get; set; }
        public string FirsName { get; set; } = "";
        public string LastName { get; set;} = "";
        public int GenderId { get; set; } = (int) Genders.Unknown;

    }
}
