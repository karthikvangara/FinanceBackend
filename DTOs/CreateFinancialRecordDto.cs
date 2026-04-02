using System;

namespace FinanceBackend.DTOs
{
    public class CreateFinancialRecordDto
    {
        public int userId {  get; set; }
        public int categoryId { get; set; }
        public decimal amount { get; set; }
        public DateTime date { get; set; }

        public string? note { get; set;}

    }
}
