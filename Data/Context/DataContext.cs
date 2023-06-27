using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementAPI.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Data.Context
{
    public class DataContext : DbContext
    {   
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.BorrowedBooks)
                .WithMany(b => b.BorrowedByUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "BorrowedBooks",
                    j => j
                        .HasOne<Book>()
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                );

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Author)
                .WithMany(a => a.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    j => j
                        .HasOne<Author>()
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Book>()
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}