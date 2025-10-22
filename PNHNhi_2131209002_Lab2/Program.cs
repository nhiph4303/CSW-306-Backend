using System.Security.Cryptography.X509Certificates;
using System;
using PNHNhi_2131209002_Lab2.Models;
using System.Runtime.CompilerServices;

namespace PNHNhi_2131209002_Lab2{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Book ===");
            Book b1 = new Book();
            Book b2 = new Book("978-0132350884", "Clean Code", "Robert C. Martin", 2008, 5);

            b1.DisplayInfo();
            b2.DisplayInfo();

            var m1 = new Member("M001", "Alice", "alice@example.com");

            // Premium member (hết hạn sau 6 tháng)
            var m2 = new PremiumMember("P001", "Bob", "bob@example.com", DateTime.Now.AddMonths(6));

            Console.WriteLine("=== Regular Member ===");
            m1.DisplayInfo();

            Console.WriteLine("\n=== Premium Member ===");
            m2.DisplayInfo();
        }
    }
}
