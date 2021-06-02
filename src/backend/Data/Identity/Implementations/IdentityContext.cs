using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using src.backend.Data;
using Models.Entities;

namespace Data.Identity.Implementations
{
    public class IdentityContext : IdentityDbContext<User, Role, Guid>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.Entity<User>(builder =>
            {
                builder.Property(user => user.Id).HasDefaultValueSql("uuid_generate_v4()");
            });
            modelBuilder.Entity<Role>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("uuid_generate_v4()");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}