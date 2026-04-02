using System;

namespace FinanceBackend.Models
{
    public class Category
    {
        public int categoryId { get; set; }
        public string name { get; set; }
        public CategoryType type { get; set; }
    }
}
