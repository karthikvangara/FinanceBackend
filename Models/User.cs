using System;

namespace FinanceBackend.Models
{
    public class User
    {
        public int userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public bool isActive { get; set; }
        public DateTime createdAt { get; set; }
    }
}
