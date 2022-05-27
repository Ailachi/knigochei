using Knigochei.Models;
using Knigochei.Repository.CartRepo;
using Knigochei.Repository.OrderRepo;
using Knigochei.UnitOfWorkDapper;

namespace Knigochei.Services.OrderService
{
	public class OrderService : IOrderService
	{
		private IUnitOfWork _uow;

		public OrderService(IUnitOfWork uow)
		{
			_uow = uow;
		}
		public void CreateNewOrder(Order order)
		{
			IOrderRepository repo = _uow.OrderRepository;

			repo.CreateOrder(order);
			_uow.Commit();

			

		}

		public void CreateOrderItemsFromCartItems(Order order, List<CartItem> cartItems)
		{
			IOrderRepository repo = _uow.OrderRepository;

			var orderItems = new List<OrderItem>();
			foreach(var cartItem in cartItems)
			{
				OrderItem orderItem = new OrderItem();
				orderItem.Amount = cartItem.Amount;
				orderItem.BookId = cartItem.BookId;
				orderItem.OrderId = order.Id;

				orderItems.Add(orderItem);
			}


			repo.AddOrderItems(orderItems);
			_uow.Commit();
		}
	}
}
