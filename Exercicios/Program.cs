using System.Runtime.CompilerServices;
using System;
using System.Globalization;
using Exercicios.Entities;
using Exercicios.Entities.Enums;

namespace Exercicios
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order {
                Id = 1080,
                Moment = DateTime.Now,
                Status = OrderStatus.SHIPPED                
            };
            Console.WriteLine(order);

            string txt = OrderStatus.PENDING_PAYMENT.ToString();
            Console.WriteLine(txt);

            OrderStatus os = Enum.Parse<OrderStatus>("DELIVERED");
            Console.WriteLine(os);
        }
    }
}
