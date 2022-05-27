using Knigochei.Models;

namespace Knigochei.ViewModels
{
	public class OrderViewModel
	{
		public IEnumerable<OrderItemViewModel> OrderItems { get; set; }
		public User User { get; set; }
		public string PickUpAddress { get; set; }
		public int TotalPrice { get; set; }

	}
}
