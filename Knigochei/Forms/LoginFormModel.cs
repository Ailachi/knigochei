using System.ComponentModel.DataAnnotations;

namespace Knigochei.Forms
{
    public class LoginFormModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "The password should contain minimum 8 charachters!")]
        public string Password { get; set; }

    }
}
