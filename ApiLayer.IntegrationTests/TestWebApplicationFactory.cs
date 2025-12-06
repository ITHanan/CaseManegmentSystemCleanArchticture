using ApiLayer;
using DomainLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InfrastructureLayer.Data;

namespace ApiLayer.IntegrationTests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove existing SQL registrations
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // Create unique DB name per test
                var dbName = $"TestDb_{Guid.NewGuid()}";

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase(dbName);
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();

                // Seed Users
                db.Users.Add(new User
                {
                    Id = 1,
                    UserName = "mimi",
                    FirstName = "Mimi",
                    LastName = "User",
                    PhoneNumber = "0700111222",
                    UserEmail = "mimi@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Mimi123@"),
                    Role = "User"
                });

                db.Users.Add(new User
                {
                    Id = 2,
                    UserName = "admin",
                    FirstName = "Admin",
                    LastName = "Master",
                    PhoneNumber = "0700333444",
                    UserEmail = "admin@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123@"),
                    Role = "Admin"
                });

                // Seed client
                db.Clients.Add(new Client
                {
                    Id = 1,
                    Name = "Test Client",
                    PhoneNumber = "0700000000",
                    Email = "client@test.com"
                });

                // Seed case
                db.Cases.Add(new Case
                {
                    Id = 1,
                    Title = "Test Case",
                    ClientId = 1,
                    CreatedByUserId = 1,
                    AssignedToUserId = 1,
                    Status = CaseStatus.Open,
                    CreatedAt = DateTime.UtcNow
                });

                db.SaveChanges();
            });
        }
    }
}
