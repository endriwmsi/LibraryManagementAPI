using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementAPI.Data.Types
{
    public class AuthorBookMap : IEntityTypeConfiguration<AuthorBook>
    {
        public void Configure(EntityTypeBuilder<AuthorBook> builder)
        {
            builder.ToTable("author_book");

            builder.Property(i => i.Id)
                .HasColumnName("id");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.AuthorId).IsRequired();
            builder.Property(i => i.AuthorId).HasColumnName("author_id");
            builder.HasOne(i => i.Author)
                .WithMany(i => i.Books)
                .HasForeignKey(i => i.AuthorId);

            builder.Property(i => i.BookId).IsRequired();
            builder.Property(i => i.BookId).HasColumnName("book_id");
            builder.HasOne(i => i.Book)
                .WithMany(i => i.Authors)
                .HasForeignKey(i => i.BookId);
        }
    }
}