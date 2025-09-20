using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using task.Data;
using task.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext db = new();
            // 1 - List all customers' first and last names along with their email addresses.
            var customers = db.Customers.AsEnumerable();
            foreach (var customer in customers)
            {
                System.Console.WriteLine($"{customer.FirstName} {customer.LastName} - {customer.Email}");
            }

            // 2- Retrieve all orders processed by a specific staff member (e.g., staff_id = 3)
            var ordersByStaff = db.Orders.Where(o => o.StaffId == 3).AsEnumerable();
            foreach (var order in ordersByStaff)
            {
                System.Console.WriteLine($"Order ID: {order.OrderId},Status: {order.OrderStatus}, Order Date: {order.OrderDate}");
            }

            // 3 - Get all products that belong to a category named "Mountain Bikes"
            var ordersBycategory = db.Products.Where(p => p.Category.CategoryName == "Mountain Bikes").AsEnumerable();
            foreach (var product in ordersBycategory)
            {
                System.Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.ProductName}, Price: {product.ListPrice}");
            }

            //4 - Count the total number of orders per store
            var orders = db.Orders.GroupBy(e => e.StoreId).Select(e => new
            {
                e.Key,
                count = e.Count()
            });
            foreach (var order in orders)
            {
                System.Console.WriteLine($"Store ID: {order.Key}, Order Count: {order.count}");
            }

            // -List all orders that have not been shipped yet(shipped_date is null)
            var orders1 = db.Orders.Where(db => db.ShippedDate == null);
            foreach (var order in orders1)
            {
                System.Console.WriteLine($"Order ID: {order.OrderId}, Order Status: {order.OrderStatus}, Order Date: {order.OrderDate},ordershipped {order.ShippedDate}");
            }

            //6 - Display each customer’s full name and the number of orders they have placed
            var customersbyorders = db.Orders.GroupBy(e => new { e.Customer.FirstName, e.Customer.LastName }).Select(e => new
            {
                e.Key.FirstName,
                e.Key.LastName,
                count = e.Count()
            });
            foreach (var customer in customersbyorders)
            {
                System.Console.WriteLine($"Customer name: {customer.FirstName} {customer.LastName}, Order Count: {customer.count}");
            }

            // List all products that have never been ordered (not found in order_items).
            var products = db.Products.Where(p => !db.OrderItems.Any(oi => oi.ProductId == p.ProductId));
            foreach (var product in products)
            {
                System.Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.ProductName}, Price: {product.ListPrice}");
            }

            //8- Display products that have a quantity of less than 5 in any store stock
            var products1 = db.Products.Where(p => p.productionstocks.Quantity < 5);
            foreach (var product in products1)
            {
                System.Console.WriteLine($"Product ID: {product.Product.ProductId}, Name: {product.Product.ProductName}, Price: {product.Product.ListPrice}");
            }

            var products3 = db.Products.Find(1);
            System.Console.WriteLine($"Product ID: {products3.ProductId}, Name: {products3.ProductName}, Price: {products3.ListPrice}");

            //10 - Retrieve all products from the products table with a certain model year
            var products4 = db.Products.Where(p => p.ModelYear == 2017);
            foreach (var product in products4)
            {
                System.Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.ProductName}, Price: {product.ListPrice}");
            }

            //11- Display each product with the number of times it was ordered
            var products2 = db.Products.Include(p => p.OrderItems).Select(p => new
            {
                p.ProductName,
                OrderCount = p.OrderItems.Count
            });
            foreach (var product in products2)
            {
                System.Console.WriteLine($"Product Name: {product.ProductName}, Order Count: {product.OrderCount}");
            }

            //12- Count the number of products in a specific category.
            var products5 = db.Products.Where(p => p.CategoryId == 2);
            foreach (var product in products5)
            {
                System.Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.ProductName}, Price: {product.ListPrice}");
            }
            Console.WriteLine(products5.Count());

            //13- Calculate the average list price of products
            var products6 = db.Products.Average(e => e.ListPrice);
            Console.WriteLine(products6);

            //14- Retrieve a specific product from the products table by ID
            var productById = db.Products.Find(1);

            //15- List all products that were ordered with a quantity greater than 3 in any order.
            var products7 = db.OrderItems.Include(e => e.Product).Where(e => e.Quantity > 3);
            foreach (var product in products7)
            {
                System.Console.WriteLine($"Product Name: {product.Product.ProductName}, Quantity: {product.Quantity}");
            }

            //16- Display each staff member’s name and how many orders they processed.
            var staffs = db.Orders.Include(o => o.Staff).GroupBy(e => new { e.Staff.FirstName, e.Staff.LastName }).Select(e => new
            {
                e.Key.FirstName,
                e.Key.LastName,
                count = e.Count()
            });
            foreach (var staff in staffs)
            {
                System.Console.WriteLine($"Staff name: {staff.FirstName} {staff.LastName}, Order Count: {staff.count}");
            }

            //17- List active staff members only (active = true) along with their phone numbers
            var staffs1 = db.Staffs.Where(s => s.Active == 1).AsEnumerable();
            foreach (var staff in staffs1)
            {
                System.Console.WriteLine($" phone: {staff.Phone}, Active: {staff.Active}");
            }

            //18- List all products with their brand name and category name.
            var products8 = db.Products.Select(e => new { e.ProductName, e.Brand.BrandName, e.Category.CategoryName });
            foreach (var product in products8)
            {
                System.Console.WriteLine($"proudectname {product.ProductName},Brand Name: {product.BrandName}, Category Name: {product.CategoryName}");
            }

            //19- Retrieve orders that are completed.
            var orders2 = db.Orders.Where(o => o.OrderStatus == 4).AsEnumerable();
            foreach (var order in orders2)
            {
                System.Console.WriteLine($"Order ID: {order.OrderId},Status: {order.OrderStatus}, Order Date: {order.OrderDate}");
            }

            //20- List each product with the total quantity sold (sum of quantity from order_items)
            var products9 = db.OrderItems.GroupBy(p => p.Product.ProductName).Select(e => new
            {
                e.Key,
                count = e.Sum(p => p.Quantity)
            });
            foreach (var product in products9)
            {
                System.Console.WriteLine($"Product Name: {product.Key}, Quantity: {product.count}");
            }


        }

    }
}
