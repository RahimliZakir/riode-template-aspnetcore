using Microsoft.EntityFrameworkCore;
using Riode.Template.WebUI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.DataContext
{
    public class RiodeDbContext : DbContext
    {
        public RiodeDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Contact>()
                   .Property(c => c.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");

            builder.Entity<Subscribe>()
                   .Property(c => c.SubscribeDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");
        }
    }
}
