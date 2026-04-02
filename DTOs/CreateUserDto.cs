using System;

namespace FinanceBackend.DTOs
{
    public class CreateUserDto
    {
        public string name { get; set; }
        public string email { get; set; }
        public string role { get; set; }
    }
}
