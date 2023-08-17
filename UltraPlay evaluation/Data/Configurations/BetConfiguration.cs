﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using UltraPlay_evaluation.Data;
using UltraPlay_evaluation.Data.Entities;

namespace UltraPlay_evaluation.Data.Configurations
{
    public partial class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> entity)
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Match)
                .WithMany(p => p.Bets)
                .HasForeignKey(d => d.MatchID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Bets_Matches");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Bet> entity);
    }
}
