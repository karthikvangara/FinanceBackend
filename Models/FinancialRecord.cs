using System;

namespace FinanceBackend.Models
{
    public class FinancialRecord
    {
        public int financialRecordId { get; set; }
        public int userId { get; set; }
        public int categoryId { get; set; }
        public decimal amount { get; set; }
        public DateTime date { get; set; }
        public string? note { get; set; }
        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }  
        public Category Category { get; set; }

    }
}