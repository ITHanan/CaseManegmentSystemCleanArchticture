using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.User.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public string FullName { get; set; } = default!; 

        public string UserEmail { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!; 

    }
}
