using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using golf.Core.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace golf.Core.Models
{
    public class golfdbContext: IdentityDbContext
    {
        private IConfiguration config;
        public golfdbContext()
        {
        }

        public golfdbContext(DbContextOptions<golfdbContext> options) :base(options)
        {
        }

        public virtual DbSet<Player> Player { get; set;}
        public virtual DbSet<Hole> Hole { get; set; }
        public virtual DbSet<Score> Score { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<PlayerHoleScore> PlayerHoleScore { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=golfDb_dev;Trusted_Connection=True;");
           // optionsBuilder.UseSqlServer("Server=ws1.dankospark.co.za\\ws1staging;Database=golfDb_dev;User Id=bsjc;Password=Jaap777?");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(golfdbContext).Assembly);
        }
    }
}
