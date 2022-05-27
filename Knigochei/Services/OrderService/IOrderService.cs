using Knigochei.Models;

namespace Knigochei.Services.OrderService
{
	public interface IOrderService
	{
		public void CreateNewOrder(Order order);
		public void CreateOrderItemsFromCartItems(Order order, List<CartItem> cartItems);
		public List<Order> GetOrdersByUser(int userId);
	}
}
