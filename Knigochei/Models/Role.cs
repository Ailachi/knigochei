namespace Knigochei.Models
{
    public class Role
    {
        private int Id { get; set; }
        private string RoleName { get; set; }
    }

    public enum Roles
    {
        Admin = 1,
        Customer
    }
}
