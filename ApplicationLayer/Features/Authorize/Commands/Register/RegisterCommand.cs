using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Authorize.Commands.Register
{
    public class RegisterCommand : IRequest<OperationResult<string>>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }   // NEW
        public string LastName { get; set; }    // NEW
        public string PhoneNumber { get; set; } // NEW
        public string UserEmail { get; set; }
        public string Password { get; set; }

        public RegisterCommand(
            string userName,
            string firstName,
            string lastName,
            string phoneNumber,
            string userEmail,
            string password)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            UserEmail = userEmail;
            Password = password;
        }
    }
}
