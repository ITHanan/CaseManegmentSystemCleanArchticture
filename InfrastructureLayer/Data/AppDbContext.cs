using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Case> Cases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CaseNote> CaseNotes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CaseTag> CaseTags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------------------------------
            // Case ↔ Tag (Many-to-Many)
            // -------------------------------
            modelBuilder.Entity<CaseTag>()
                .HasKey(ct => new { ct.CaseId, ct.TagId });

            modelBuilder.Entity<CaseTag>()
                .HasOne(ct => ct.Case)
                .WithMany(c => c.CaseTags)
                .HasForeignKey(ct => ct.CaseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CaseTag>()
                .HasOne(ct => ct.Tag)
                .WithMany(t => t.CaseTags)
                .HasForeignKey(ct => ct.TagId)
                .OnDelete(DeleteBehavior.Cascade);



            // -------------------------------
            // Case → Client (Many-to-One)
            // -------------------------------
            modelBuilder.Entity<Case>()
                .HasOne(c => c.Client)
                .WithMany(cl => cl.Cases)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);


            // Case → CreatedByUser (Many-to-One)
            modelBuilder.Entity<Case>()
                .HasOne(c => c.CreatedByUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Case → AssignedTo (Many-to-One, Nullable)
            modelBuilder.Entity<Case>()
                .HasOne(c => c.AssignedTo)
                .WithMany(u => u.AssignedCases)
                .HasForeignKey(c => c.AssignedToUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Case → UpdatedByUser (Many-to-One, Nullable)
            modelBuilder.Entity<Case>()
                .HasOne(c => c.UpdatedByUser)
                .WithMany()
                .HasForeignKey(c => c.UpdatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);


            // -------------------------------
            // Case → Notes (One-to-Many)
            // -------------------------------
            modelBuilder.Entity<CaseNote>()
                .HasOne(n => n.Case)
                .WithMany(c => c.Notes)
                .HasForeignKey(n => n.CaseId)
                .OnDelete(DeleteBehavior.Cascade);


            // -------------------------------
            // Optional: Field Limits
            // -------------------------------
            modelBuilder.Entity<Case>()
                .Property(c => c.Title)
                .HasMaxLength(200);

            modelBuilder.Entity<User>()
                .Property(u => u.UserEmail)
                .HasMaxLength(200);
        }
    }
}
