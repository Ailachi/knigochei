namespace Knigochei.Models
{
    public class Gender
    {
        private int Id { get; set; }
        private string GenderName { get; set; }

    }

    public enum Genders
    {
        Female = 1,
        Male,
        Unknown
    }
}
