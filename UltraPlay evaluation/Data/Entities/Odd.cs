﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UltraPlay_evaluation.Data.Entities
{
    public partial class Odd : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Value { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal SpecialBetValue { get; set; }
        public int? BetID { get; set; }

        [ForeignKey("BetID")]
        [InverseProperty("Odds")]
        public virtual Bet Bet { get; set; }
    }
}