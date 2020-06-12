using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicShare.Models;

namespace MusicShare.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Genere> Genere { get; set; }
        public DbSet<Canzone> Canzone { get; set; }
        public DbSet<Raccolta> Raccolta { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RaccoltaCanzoni>().HasKey(pt => new { pt.Id_Raccolta, pt.Id_Canzone });

            builder.Entity<RaccoltaCanzoni>()
                .HasOne(s => s.Canzone).WithMany(s => s.RaccoltaCanzoni)
                .HasForeignKey(s => s.Id_Canzone);

            builder.Entity<RaccoltaCanzoni>()
               .HasOne(s => s.Raccolta).WithMany(s => s.RaccoltaCanzoni)
               .HasForeignKey(s => s.Id_Raccolta);
        }

        public DbSet<MusicShare.Models.RaccoltaCanzoni> RaccoltaCanzoni { get; set; }
    }
}
