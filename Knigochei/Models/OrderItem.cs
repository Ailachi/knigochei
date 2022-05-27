namespace Knigochei.Models
{
	public class OrderItem
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int BookId { get; set; }
		public int Amount { get; set; }
	}
}
