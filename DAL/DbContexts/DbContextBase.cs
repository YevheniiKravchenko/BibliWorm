using Common.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DAL.DbContexts
{
    public class DbContextBase : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<ReaderCard> ReaderCards { get; set; } = null!;

        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        public DbSet<ResetPasswordToken> ResetPasswordTokens { get; set; } = null!;

        public DbSet<Book> Books { get; set; } = null!;

        public DbSet<BookBooking> BookBookings { get; set; } = null!;

        public DbSet<Department> Departments { get; set; } = null!;

        public DbSet<ReservationQueue> ReservationQueues { get; set; }

        public DbContextBase(DbContextOptions<DbContextBase> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapUser(modelBuilder);
            MapBook(modelBuilder);
            AddAdmin(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void MapUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasOne(u => u.ReaderCard)
                .WithOne(p => p.User)
                .HasForeignKey<ReaderCard>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReaderCard>()
                .HasKey(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.RefreshTokens)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RefreshToken>()
                .Property(x => x.RefreshTokenId)
                .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<User>()
                .HasMany(x => x.ResetPasswordTokens)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.BookBookings)
                .WithOne(b => b.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ResetPasswordToken>()
                .Property(x => x.ResetPasswordTokenId)
                .HasValueGenerator(typeof(GuidValueGenerator));
        }

        private void MapBook(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.BookId)
                .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<Book>()
                .HasMany(b => b.ReservationQueues)
                .WithOne(b => b.Book)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.BookBookings)
                .WithOne(bb => bb.Book);

            modelBuilder.Entity<BookBooking>()
                .Property(b => b.BookBookingId)
                .HasValueGenerator(typeof(GuidValueGenerator));
        }

        private void AddAdmin(ModelBuilder modelBuidler)
        {
            modelBuidler.Entity<User>().HasData(new User
            {
                UserId = 1,
                Login = "Admin",
                PasswordHash = "$2a$10$WkrWKFdubfRwcY4MjdFELui7Dh8r3ykAvDYOQPvQud0vPlxFHVen.", // password: admin231_rte
                PasswordSalt = "d!W2~4~zI{wq:l<p",
                RegistrationDate = DateTime.UtcNow,
                Role = Role.Admin
            });

            modelBuidler.Entity<ReaderCard>().HasData(new ReaderCard
            {
                UserId = 1,
                FirstName = "Admin",
                LastName = "Admin",
                ProfilePicture = Array.Empty<byte>(),
                Address = "Street Ave. 15",
                PhoneNumber = "0555555555",
                BirthDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Email = "admin@bibliworm.com"
            });
        }
    }
}
