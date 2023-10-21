namespace eTickets.Data.Services
{
    public class OrdersService : IOrdersService

    {
        private readonly AppDbContext _context;

        public OrdersService(AppDbContext context) => _context = context;



        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {

            var orders = await _context.Orders.Include(u=>u.User).Include(o => o.OrderItems)
                .ThenInclude(o => o.Movie)
                .ToListAsync();
            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }
            return orders;
        }
        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmail)
        {
            //Try to make this using transaction
            var order = new Order()
            {
                UserId = userId,
                UserEmail = userEmail
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.MovieId,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };
                await _context.OrderItems.AddAsync(orderItem);

            }
            await _context.SaveChangesAsync();

        }
    }
}
