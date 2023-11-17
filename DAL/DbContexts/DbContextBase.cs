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

        public DbSet<BookCopy> BookCopies { get; set; } = null!;

        public DbSet<Booking> Bookings { get; set; } = null!;

        public DbSet<BookReview> BookReviews { get; set; } = null!;

        public DbSet<Department> Departments { get; set; } = null!;

        public DbSet<DepartmentStatistics> DepartmentStatistics { get; set; } = null!;

        public DbSet<Domain.Models.Enum> Enums { get; set; } = null!;

        public DbSet<EnumItem> EnumItems { get; set; } = null!;

        public DbSet<ReservationQueue> ReservationQueues { get; set; } = null!;

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
            MapKeys(modelBuilder);
            SetGuidValueGeneratorForEntities(modelBuilder);
            MapUser(modelBuilder);
            MapBook(modelBuilder);
            MapDepartment(modelBuilder);
            AddAdmin(modelBuilder);
            AddGenres(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void MapKeys(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Book>()
                .HasKey(b => b.BookId);

            modelBuilder.Entity<BookCopy>()
                .HasKey(bc => bc.BookCopyId);

            modelBuilder.Entity<Booking>()
                .HasKey(b => b.BookingId);

            modelBuilder.Entity<BookReview>()
                .HasKey(br => br.BookReviewId);

            modelBuilder.Entity<Department>()
                .HasKey(d => d.DepartmentId);

            modelBuilder.Entity<DepartmentStatistics>()
                .HasKey(ds => ds.DepartmentStatisticsId);

            modelBuilder.Entity<Domain.Models.Enum>()
                .HasKey(e => e.EnumID);

            modelBuilder.Entity<EnumItem>()
                .HasKey(ei => ei.EnumItemId);

            modelBuilder.Entity<ReaderCard>()
                .HasKey(p => p.UserId);

            modelBuilder.Entity<RefreshToken>()
                .HasKey(rt => rt.RefreshTokenId);

            modelBuilder.Entity<ReservationQueue>()
                .HasKey(rq => rq.ReservationQueueId);

            modelBuilder.Entity<ResetPasswordToken>()
                .HasKey(rpt => rpt.ResetPasswordTokenId);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
        }

        private void SetGuidValueGeneratorForEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.BookId)
                .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<BookCopy>()
                .Property(bc => bc.BookCopyId)
                .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<Booking>()
                .Property(b => b.BookingId)
                .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<BookReview>()
                .Property(br => br.BookReviewId)
                .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<DepartmentStatistics>()
                .Property(ds => ds.DepartmentStatisticsId)
                .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<RefreshToken>()
               .Property(rt => rt.RefreshTokenId)
               .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<ReservationQueue>()
                .Property(rq => rq.ReservationQueueId)
                .HasValueGenerator(typeof(GuidValueGenerator));

            modelBuilder.Entity<ResetPasswordToken>()
               .Property(rpt => rpt.ResetPasswordTokenId)
               .HasValueGenerator(typeof(GuidValueGenerator));
        }

        private void MapUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.ReaderCard)
                .WithOne(rc => rc.User)
                .HasForeignKey<ReaderCard>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ResetPasswordTokens)
                .WithOne(rpt => rpt.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReservationQueues)
                .WithOne(rq => rq.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.BookReviews)
                .WithOne(br => br.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.FavouriteBooks)
                .WithMany(b => b.Users);
        }

        private void MapBook(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(b => b.ReservationQueues)
                .WithOne(rq => rq.Book)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.BookReviews)
                .WithOne(br => br.Book)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.BookCopies)
                .WithOne(bc => bc.Book)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books);
        }

        private void MapDepartment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Books)
                .WithOne(b => b.Department)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.DepartmentStatistics)
                .WithOne(ds => ds.Department)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void AddAdmin(ModelBuilder modelBuidler)
        {
            modelBuidler.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Login = "Admin",
                    PasswordHash = "$2a$10$WkrWKFdubfRwcY4MjdFELui7Dh8r3ykAvDYOQPvQud0vPlxFHVen.", // password: admin231_rte
                    PasswordSalt = "d!W2~4~zI{wq:l<p",
                    RegistrationDate = DateTime.UtcNow,
                    Role = Role.Admin
                }
            );

            modelBuidler.Entity<ReaderCard>().HasData(
                new ReaderCard
                {
                    UserId = 1,
                    FirstName = "Admin",
                    LastName = "Admin",
                    ProfilePicture = Array.Empty<byte>(),
                    Address = "Street Ave. 15",
                    PhoneNumber = "0555555555",
                    BirthDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Email = "admin@bibliworm.com"
                }
            );
        }

        private void AddGenres(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Models.Enum>().HasData(
                new Domain.Models.Enum
                {
                    EnumID = 1,
                    Code = "Genre",
                }
            );

            modelBuilder.Entity<EnumItem>().HasData(
                new EnumItem
                {
                    EnumItemId = 1,
                    Value = "Mystery",
                    EnumId = 1,
                },
                new EnumItem
                {
                    EnumItemId = 2,
                    Value = "Fantasy",
                    EnumId = 1,
                },
                new EnumItem
                {
                    EnumItemId = 3,
                    Value = "Science fiction",
                    EnumId = 1,
                },
                new EnumItem
                {
                    EnumItemId = 4,
                    Value = "Adventure",
                    EnumId = 1,
                }
            );
        }
    }
}
