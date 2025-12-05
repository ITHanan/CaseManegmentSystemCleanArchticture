using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public string? UserEmail { get; set; }
        public string PhoneNumber { get; set; } = default!;

        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = "User";  

        public ICollection<Case> AssignedCases { get; set; } = new List<Case>();
    }
}
