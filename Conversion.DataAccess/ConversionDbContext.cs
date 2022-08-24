using System.Collections.Generic;
using Conversion.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Conversion.DataAccess
{
    public class ConversionDbContext : IdentityDbContext<ApplicationUser>
    {
        public ConversionDbContext()
        {
            
        }

        public ConversionDbContext(DbContextOptions<ConversionDbContext> options) : base(options)
        {

        }

        public virtual DbSet<ConversionHistory> ConversionHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConversionHistoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}