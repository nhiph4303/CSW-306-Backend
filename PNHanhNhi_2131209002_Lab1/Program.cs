// See https://aka.ms/new-console-template for more information
/*
    Name: Phan Ngoc Hanh Nhi
    Student ID: 2131209002
    Lab 1 – Introduction to C# Programming
    Demonstrates: variables, methods, loops, and conditional statements
*/

namespace PNHNhi_2131209002_Lab1
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Q2
            Console.WriteLine("Question 2");
            Console.WriteLine("Hello World");

            // Q3
            Console.WriteLine("Question 3");
            Console.WriteLine("Enter number A: ");
            int numberA = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter number B: ");
            int numberB = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"The sum of {numberA} and {numberB} is {AddTwoNumbers(numberA, numberB)}");


            // Q4
            Console.WriteLine("Question 4");
            Console.WriteLine($"Before swap: A = {numberA}, B = {numberB}");
            swapNumber(ref numberA, ref numberB);
            Console.WriteLine($"After swap:  A = {numberA}, B = {numberB}");

            //Q5
            Console.WriteLine("Question 5");
            Console.WriteLine("Enter the scrore: ");
            double averageScore = Convert.ToDouble(Console.ReadLine());
            ClassifyStudent(averageScore);


            //Q6
            Console.WriteLine("Question 6");
            Console.WriteLine("Enter the month: ");
            int month = Convert.ToInt32(Console.ReadLine());
            PrintMonthInfo(month);

            //Q7
            Console.WriteLine("Question 7");
            Console.WriteLine("Enter a number n: ");
            int n = Convert.ToInt32(Console.ReadLine());
            SumToN(n);



        }
        static void SumToN(int n)
        {
            if (n <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive number.");
                return;
            }

            int sum = 0;
            for (int i = 1; i <= n; i++)
            {
                sum += i;
            }

            Console.WriteLine($"The sum from 1 to {n} is {sum}");
        }

        static void PrintMonthInfo(int month)
        {
            if (month < 1 || month > 12)
            {
                Console.WriteLine("The month input is invalid.");
                return;
            }

            string monthName;
            int days;

            switch (month)
            {
                case 1: monthName = "January"; days = 31; break;
                case 2: monthName = "February"; days = 28; break;
                case 3: monthName = "March"; days = 31; break;
                case 4: monthName = "April"; days = 30; break;
                case 5: monthName = "May"; days = 31; break;
                case 6: monthName = "June"; days = 30; break;
                case 7: monthName = "July"; days = 31; break;
                case 8: monthName = "August"; days = 31; break;
                case 9: monthName = "September"; days = 30; break;
                case 10: monthName = "October"; days = 31; break;
                case 11: monthName = "November"; days = 30; break;
                case 12: monthName = "December"; days = 31; break;
                default: monthName = "Invalid"; days = 0; break;
            }

            Console.WriteLine($"{monthName} - Have {days} days.");
        }

        static void ClassifyStudent(double averageScore)
        {
            if (averageScore < 0 || averageScore > 100)
            {
                Console.WriteLine("Invalid score");
            }
            else if (averageScore < 70)
            {
                Console.WriteLine("Average");
            }
            else if (averageScore < 80)
            {
                Console.WriteLine("Fair");
            }
            else if (averageScore < 90)
            {
                Console.WriteLine("Good");
            }
            else
            {
                Console.WriteLine("Excellent");
            }

        }

        static void swapNumber(ref int numberA, ref int numberB)
        {
            int temp = numberA;
            numberA = numberB;
            numberB = temp;
        }

        static int AddTwoNumbers(int numberA, int numberB)
        {
            return numberA + numberB;
        }
    }
}