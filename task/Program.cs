using task.Data;

namespace task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext db = new();
            // 1 - List all customers' first and last names along with their email addresses.
            //var customers = db.Customers.AsEnumerable();
            //foreach (var customer in customers)
            //{
            //    System.Console.WriteLine($"{customer.FirstName} {customer.LastName} - {customer.Email}");
            //}

            // 2- Retrieve all orders processed by a specific staff member (e.g., staff_id = 3)
            //var ordersByStaff = db.Orders.Where(o => o.StaffId == 3).AsEnumerable();
            //foreach (var order in ordersByStaff)
            //{
            //    System.Console.WriteLine($"Order ID: {order.OrderId},Status: {order.OrderStatus}, Order Date: {order.OrderDate}");
            //}

            // 3 - Get all products that belong to a category named "Mountain Bikes"
            //var ordersBycategory = db.Products.Where(p => p.Category.CategoryName == "Mountain Bikes").AsEnumerable();
            //foreach (var product in ordersBycategory)
            //{
            //    System.Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.ProductName}, Price: {product.ListPrice}");
            //}

            var orders = db.Orders.OrderBy(e => e.StoreId).Select(e => new
            {
                e.key,
                count = e.count()
            });
            foreach (var order in orders)
            {
                System.Console.WriteLine($"Store ID: {order.key}, Order Count: {order.count}");
            }
        }
    }
}
