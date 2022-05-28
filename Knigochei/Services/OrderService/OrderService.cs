using Knigochei.Models;
using Knigochei.Repository.CartRepo;
using Knigochei.Repository.OrderRepo;
using Knigochei.UnitOfWorkDapper;
using Microsoft.Office.Interop.Excel;

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

        public List<Order> GetAllOrders()
        {
			IOrderRepository repo = _uow.OrderRepository;
			List<Order> orders = repo.All().ToList();

			return orders ?? new List<Order>();
		}

		public List<Order> GetOrdersByUser(int userId)
        {
			IOrderRepository repo = _uow.OrderRepository;
			List<Order> userOrders = repo.GetOrdersByUserId(userId).ToList();

			return userOrders ?? new List<Order>();
		}

        public void SaveOrdersInExcel(List<Order> orders, string filePath)
        {
			Application application = 
				new Microsoft.Office.Interop.Excel.Application();
			if (application == null)
			{
				throw new NullReferenceException();
			}

            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
			object misValue = System.Reflection.Missing.Value;

			xlWorkBook = application.Workbooks.Add(misValue);
			xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

			xlWorkSheet.Cells[1, 1] = "Id";
			xlWorkSheet.Cells[1, 2] = "Order Date";
			xlWorkSheet.Cells[1, 3] = "Delivery Date";
			xlWorkSheet.Cells[1, 4] = "Pick Up Address";
			xlWorkSheet.Cells[1, 5] = "Total Price";

			for (int i = 2; i < orders.Count; i++)
            {
				xlWorkSheet.Cells[i, 1] = orders[i].Id;
				xlWorkSheet.Cells[i, 2] = orders[i].OrderDate;
				xlWorkSheet.Cells[i, 3] = orders[i].DeliveryDate;
				xlWorkSheet.Cells[i, 4] = orders[i].PickUpAddress;
				xlWorkSheet.Cells[i, 5] = orders[i].TotalPrice;
			}

			xlWorkBook.SaveAs(filePath);
			xlWorkBook.Close(true);
			application.Quit();

		}

    }
}
