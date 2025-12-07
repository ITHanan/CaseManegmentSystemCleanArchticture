using ApiLayer;
using InfrastructureLayer.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DomainLayer.Models;

namespace ApiLayer.IntegrationTests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // REMOVE the real SQL Server registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // ADD InMemory database
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                // Build service provider
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();

                // SEED USERS
                db.Users.Add(new User
                {
                    Id = 1,
                    UserName = "mimi",
                    UserEmail = "mimi@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Mimi123@"),
                    Role = "User",
                    PhoneNumber = "0700000000",
                    FirstName = "Mimi",
                    LastName = "Test"
                });

                db.Users.Add(new User
                {
                    Id = 2,
                    UserName = "admin",
                    UserEmail = "admin@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123@"),
                    Role = "Admin",
                    PhoneNumber = "0700000000",
                    FirstName = "Admin",
                    LastName = "User"
                });

                // SEED CLIENT
                db.Clients.Add(new Client
                {
                    Id = 1,
                    Name = "Test Client",
                    Email = "client@test.com",
                    PhoneNumber = "0700000000"
                });

                // SEED CASE
                db.Cases.Add(new Case
                {
                    Id = 1,
                    Title = "Sample Case",
                    ClientId = 1,
                    CreatedByUserId = 1,
                    AssignedToUserId = 1,
                    Status = CaseStatus.Open
                });

                db.SaveChanges();
            });
        }
    }
}
