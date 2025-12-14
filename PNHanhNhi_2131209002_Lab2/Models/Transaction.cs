using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNHNhi_2131209002_Lab2.Models
{
    public abstract class Transaction
    {
        public string TransactionID { get; set; } = Guid.NewGuid().ToString();

        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public Member Member { get; set; }

        public Transaction() { }

        protected Transaction(Member member)
        {
            Member = member ?? throw new ArgumentNullException(nameof(member));
        }

        public abstract void Execute();
    }
}
