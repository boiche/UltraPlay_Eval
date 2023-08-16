﻿using Microsoft.EntityFrameworkCore;
using UltraPlay_evaluation.Data.Entities;
#nullable disable

namespace UltraPlay_evaluation.Data
{
    public partial class UltraPlay_EvalContext : DbContext
    {
        public UltraPlay_EvalContext()
        {
        }

        public UltraPlay_EvalContext(DbContextOptions<UltraPlay_EvalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bet> Bets { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Odd> Odds { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-F7BG25T\\SQLEXPRESS;Initial Catalog=UltraPlay_Eval;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.BetConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.EventConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.MatchConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.OddConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SportConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
