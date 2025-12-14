using System;

namespace PNHNhi_2131209002_Lab2.Models
{
    public class LibraryCard
    {
        public string CardNumber { get; init; }
        public Member Owner { get; set; }
        public DateTime IssueDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        private bool isDeactivated;

        public bool IsActive => !isDeactivated && DateTime.Now < ExpiryDate;

        public LibraryCard(string cardNumber, Member owner)
        {
            if (string.IsNullOrEmpty(cardNumber))
                throw new ArgumentException("Card number cannot be empty.");

            CardNumber = cardNumber;
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            IssueDate = DateTime.Now;

            //xác định hạn thẻ
            if (owner is PremiumMember)
                ExpiryDate = IssueDate.AddYears(2);
            else
                ExpiryDate = IssueDate.AddYears(1);
        }

        //gia hạn
        public void RenewCard()
        {
            IssueDate = DateTime.Now;

            if (Owner is PremiumMember)
                ExpiryDate = IssueDate.AddYears(2);
            else
                ExpiryDate = IssueDate.AddYears(1);

            isDeactivated = false;
            Console.WriteLine($"Card {CardNumber} renewed successfully!");
        }

        //hủy
        public void DeactivateCard()
        {
            isDeactivated = true;
            Console.WriteLine($"Card {CardNumber} has been deactivated.");
        }

        //show
        public void DisplayCardInfo()
        {
            Console.WriteLine("\n---- Library Card Info ----");
            Console.WriteLine($"Card Number: {CardNumber}");
            Console.WriteLine($"Owner: {Owner.Name} ({Owner.MemberID})");
            Console.WriteLine($"Issue Date: {IssueDate:yyyy-MM-dd}");
            Console.WriteLine($"Expiry Date: {ExpiryDate:yyyy-MM-dd}");
            Console.WriteLine($"Status: {(IsActive ? "Active" : "Inactive")}");
        }
    }
}
