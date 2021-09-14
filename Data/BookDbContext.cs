using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlothBookStore.Data 
{
    public class BookDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        public DbSet<Book> BooksSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*參考 
             * https://docs.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwithout-nrt
             */
            
            BookBuilder(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void BookBuilder(ModelBuilder builder)
        {
            builder.Entity<Book>(eb =>
            {
                eb.ToTable("Book");
                eb.HasKey(c => c.Id);
                eb.Property(c => c.Id).HasMaxLength(40);

                eb.Property(c => c.Name).HasMaxLength(500).IsRequired();
                eb.Property(c => c.Author).HasMaxLength(300).IsRequired();
                eb.Property(c => c.Desc).HasMaxLength(1000);
                eb.Property(c => c.Price).HasColumnType("decimal(9,0)");
            });
        }
    }
}
