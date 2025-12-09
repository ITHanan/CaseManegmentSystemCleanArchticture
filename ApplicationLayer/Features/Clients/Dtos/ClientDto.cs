namespace ApplicationLayer.Features.Clients.Dtos
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
