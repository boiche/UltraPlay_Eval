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
    public partial class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> entity)
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Event)
                .WithMany(p => p.Matches)
                .HasForeignKey(d => d.EventID)
                .HasConstraintName("FK_Matches_Events");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Match> entity);
    }
}
