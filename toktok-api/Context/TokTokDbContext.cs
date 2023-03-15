using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using toktok_api.Models;

namespace toktok_api.Context
{
    public class TokTokDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public TokTokDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ToktokConnectionString"));

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Movie>().HasKey(mv => mv.Id);
            modelBuilder.Entity<Reaction>().HasKey(rc => new { rc.UserId, rc.MovieId });

            modelBuilder.Entity<Reaction>()
                .HasOne<User>(rc => rc.User)
                .WithMany(u => u.Reactions)
                .HasForeignKey(rc => rc.UserId);


            modelBuilder.Entity<Reaction>()
                .HasOne<Movie>(rc => rc.Movie)
                .WithMany(s => s.Reactions)
                .HasForeignKey(rc => rc.MovieId);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
    }
}