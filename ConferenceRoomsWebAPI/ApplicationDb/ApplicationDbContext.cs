using ConferenceRoomsWebAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomsWebAPI.ApplicationDb
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ConferenceRooms> ConferenceRooms { get; set; }
        public DbSet<CompanyServices> CompanyServices { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public ApplicationDbContext(DbContextOptions ontions) : base(ontions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConferenceRooms>()
                .HasKey(cr => cr.IdRoom);

            modelBuilder.Entity<CompanyServices>()
                .HasKey(cs => cs.IdService);

            modelBuilder.Entity<Booking>()
                .HasKey(b => b.IdBooking);


            modelBuilder.Entity<ConferenceRooms>()
                .HasMany(b => b.Bookings)
                .WithOne(c => c.ConferenceRooms)
                .HasForeignKey(c => c.IdConferenceRoom)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConferenceRooms>()
                .HasIndex(cn => cn.NameRoom)
                .IsUnique();

            modelBuilder.Entity<ConferenceRooms>()
               .Property(r => r.BasePricePerHour)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

            modelBuilder.Entity<CompanyServices>()
                .HasMany(b => b.BookingCompanyServices)
                .WithOne(c => c.CompanyServices)
                .HasForeignKey(c => c.IdCompanyService);

            modelBuilder.Entity<Booking>()
                .HasMany(b => b.BookingCompanyServices)
                .WithOne(c => c.Booking)
                .HasForeignKey(c => c.IdBooking);


            modelBuilder.Entity<BookingCompanyService>()
                .HasKey(bcs => new
                {
                    bcs.IdCompanyService,
                    bcs.IdBooking
                });

            modelBuilder.Entity<Booking>()
               .Property(b => b.BookingDate)
               .IsRequired();

            modelBuilder.Entity<Booking>()
               .Property(b => b.TotalPrice)
               .HasColumnType("decimal(18,2)")
               .IsRequired();




            //modelBuilder.Entity<BookingService>()
            //    .HasOne(bs => bs.Booking) // Один Booking може мати багато Service
            //    .WithMany(b => b.BookingServices) // Booking має багато BookingService
            //    .HasForeignKey(bs => bs.BookingId);

            //modelBuilder.Entity<BookingService>()
            //    .HasOne(bs => bs.Service) // Один Service може бути в багатьох Booking
            //    .WithMany(s => s.BookingServices) // Service має багато BookingService
            //    .HasForeignKey(bs => bs.ServiceId);




            //modelBuilder.Entity<Client>()
            //    .HasKey(clientId => clientId.Id);

            //modelBuilder.Entity<Order>()
            //    .HasKey(orderId => orderId.Id);

            //modelBuilder.Entity<Client>()
            //     .HasMany(orderHistory => orderHistory.OrderHistories)
            //     .WithOne(client => client.Client)
            //     .HasForeignKey(clientId => clientId.ClientId);

            //modelBuilder.Entity<Order>()
            //     .HasMany(orderHistory => orderHistory.OrderHistories)
            //     .WithOne(order => order.Order)
            //     .HasForeignKey(orderId => orderId.OrderId); ;

            //modelBuilder.Entity<OrderHistory>()
            //    .HasKey(x => new { x.OrderId, x.ClientId });

        }
    }
}
