namespace Knigochei.Models
{
	public class Order
	{
		public int Id { get; set; }
		public int TotalPrice { get; set; }
		public DateOnly OrderDate { get; set; }
		public DateOnly DeliveryDate { get; set; }
		public string PickUpAddress { get; set; }
		public int OrderStatusId { get; set; }
		public int UserId { get; set; }
	}
}
