using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.Authorize.DTOs
{
    public class UserRegisterDto
    {
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string UserEmail { get; set; }
        public required string Password { get; set; }

        public required string Role { get; set; } = "User";
    }
}
