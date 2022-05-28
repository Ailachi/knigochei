using Knigochei.Models;

namespace Knigochei.Services.OrderService
{
	public interface IOrderService
	{
		public void CreateNewOrder(Order order);
		public void CreateOrderItemsFromCartItems(Order order, List<CartItem> cartItems);
		public List<Order> GetOrdersByUser(int userId);

		public List<Order> GetAllOrders();
		public void SaveOrdersInExcel(List<Order> orders, string filePath);
	}
}
