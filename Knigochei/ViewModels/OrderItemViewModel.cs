using Knigochei.Models;

namespace Knigochei.ViewModels
{
	public class OrderItemViewModel
	{
		public CartItem CartItem { get; set; }
		public Book Book { get; set; }
		public int TotalAmountPrice { get; set; }

	}
}
