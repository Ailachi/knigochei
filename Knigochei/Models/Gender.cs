namespace Knigochei.Models
{
    public class Gender
    {
        public int Id { get; set; }
        public string GenderName { get; set; }

    }

    public enum Genders
    {
        Female = 1,
        Male,
        Unknown
    }
}
