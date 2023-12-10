using FinalProject_ConferenceRoomBooking.Data_Layer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_ConferenceRoomBooking.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ConferenceRooms> ConferenceRooms { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<ReservationHolder> ReservationHolders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Booking and ReservationHolder
            modelBuilder.Entity<Bookings>()
                .HasOne(b => b.ReservationHolder)
                .WithOne(r => r.Bookings)
                .HasForeignKey<ReservationHolder>(r => r.BookingId);

            // ... Other configurations ...
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        internal void UpdateBooking(Bookings updatedBooking)
        {
            throw new NotImplementedException();
        }
    }
}