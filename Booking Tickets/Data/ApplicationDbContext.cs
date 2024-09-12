using Booking_Tickets.Data.Entites;
using Booking_Tickets.Helper;
using Microsoft.EntityFrameworkCore;

namespace Booking_Tickets.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
       
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserID);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Tickets)
                .WithOne(t => t.Event)
                .HasForeignKey(t => t.EventID);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Reviews)
                .WithOne(r => r.Event)
                .HasForeignKey(r => r.EventID);

            modelBuilder.Entity<Venue>()
                .HasMany(v => v.Events)
                .WithOne(e => e.Venue)
                .HasForeignKey(e => e.VenueID);

            modelBuilder.Entity<Carts>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Ticket)
                .WithMany()
                .HasForeignKey(od => od.TicketID);


            modelBuilder.Entity<UserReview>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<UserReview>()
                .HasOne(ur => ur.Event)
                .WithMany(e => e.Reviews)
                .HasForeignKey(ur => ur.EventID);
            // seed data 
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    ID=1,
                    FullName="Admin",
                    Username="Admin",
                    Password=HashPassword.Hash("Admin@123"),
                    Phone="01111111111",
                    Role=Role.UserRole.Admin,
                    CreatedAt=DateTime.UtcNow
                });
        }
   
    
    }
}
