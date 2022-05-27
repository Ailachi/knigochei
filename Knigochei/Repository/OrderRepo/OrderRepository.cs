using Knigochei.Models;
using System.Data;
using Dapper;

namespace Knigochei.Repository.OrderRepo
{
	internal class OrderRepository : RepositoryBase, IOrderRepository
	{
		public OrderRepository(IDbTransaction transaction) : base(transaction)
		{
		}

		public void AddOrderItems(List<OrderItem> orderItems)
		{
			var dt = new DataTable();
			dt.Columns.Add("OrderId", typeof(int));
			dt.Columns.Add("BookId", typeof(int));
			dt.Columns.Add("Amount", typeof(int));

			foreach(var orderItem in orderItems)
			{
				dt.Rows.Add(orderItem.OrderId, orderItem.BookId, orderItem.Amount);
			}

			int res = Connection.ExecuteScalar<int>(
				sql: "[dbo].[InsertOrderItems]",
				param: new { @orderItems = dt.AsTableValuedParameter("dbo.OrderItemType") },
				transaction: Transaction,
				commandType: CommandType.StoredProcedure
			);

		}

		public void CreateOrder(Order order)
		{
			TimeOnly orderTime = TimeOnly.FromDateTime(DateTime.Now);
			TimeOnly deliveryTime = TimeOnly.FromDateTime(DateTime.Now.AddDays(7));


			order.Id = Connection.ExecuteScalar<int>(
				sql: "INSERT INTO Orders(TotalPrice, OrderDate, DeliveryDate, PickUpAddress, OrderStatusId, UserId) " +
					 "VALUES(@price, @orderDate, @deliveryDate, @address, @status, @userId); " +
					 "SELECT SCOPE_IDENTITY();",
				param: new
				{
					@price = order.TotalPrice,
					@orderDate = order.OrderDate.ToDateTime(orderTime),
					@deliveryDate = order.DeliveryDate.ToDateTime(deliveryTime),
					@address = order.PickUpAddress,
					@status = order.OrderStatusId,
					@userId = order.UserId
				},
				transaction: Transaction
			);
        }
	}
}
