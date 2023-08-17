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
    public partial class SportConfiguration : IEntityTypeConfiguration<Sport>
    {
        public void Configure(EntityTypeBuilder<Sport> entity)
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Sport> entity);
    }
}
