using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebitelASP.Models
{
    public class ShopInitializer : DropCreateDatabaseIfModelChanges<ShopDbContext>
    {
        protected override void Seed(ShopDbContext context)
        {
            Product product1 = new Product
            {
                Name = "Artek",
                Price = 8
            };
            Product product2 = new Product
            {
                Name = "Milenium",
                Price = 35
            };
            Product product3 = new Product
            {
                Name = "Korona",
                Price = 22
            };
            context.Products.Add(product1);
            context.Products.Add(product2);
            context.Products.Add(product3);
      
            Order order1 = new Order
            {
                Number = "RE1432",
                Amount = 4
            };
            Order order2 = new Order
            {
                Number = "RE1432",
                Amount = 3
            };
            Order order3 = new Order
            {
                Number = "ZH8472",
                Amount = 7
            };
            Order order4 = new Order
            {
                Number = "ZH8472",
                Amount = 4
            };
            Order order5 = new Order
            {
                Number = "G3029KE",
                Amount = 2
            };
            context.Orders.Add(order1);
            context.Orders.Add(order2);
            context.Orders.Add(order3);
            context.Orders.Add(order4);
            context.Orders.Add(order5);

            OrderProduct orderProduct1 = new OrderProduct
            {
                OrderId = order1.Id,
                Order = order1,
                ProductId = product1.Id,
                Product = product1
            };
            OrderProduct orderProduct2 = new OrderProduct
            {
                OrderId = order2.Id,
                Order = order2,
                ProductId = product2.Id,
                Product = product2
            };
            OrderProduct orderProduct3 = new OrderProduct
            {
                OrderId = order3.Id,
                Order = order3,
                ProductId = product1.Id,
                Product = product1
            };
            OrderProduct orderProduct4 = new OrderProduct
            {
                OrderId = order4.Id,
                Order = order4,
                ProductId = product3.Id,
                Product = product3
            };
            OrderProduct orderProduct5 = new OrderProduct
            {
                OrderId = order5.Id,
                Order = order5,
                ProductId = product3.Id,
                Product = product3
            };
            context.OrderProducts.Add(orderProduct1);
            context.OrderProducts.Add(orderProduct2);
            context.OrderProducts.Add(orderProduct3);
            context.OrderProducts.Add(orderProduct4);
            context.OrderProducts.Add(orderProduct5);

            context.SaveChanges();
        }
    }
}