namespace Knigochei.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{LastName} {FirstName}"; } }
        public int GenderId { get; set; }
        public int RoleId { get; set; }


    }
}
