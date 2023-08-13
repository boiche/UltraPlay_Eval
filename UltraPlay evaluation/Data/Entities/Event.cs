﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UltraPlay_evaluation.Data.Entities
{
    public partial class Event : BaseEntity
    {
        public Event()
        {
            Matches = new HashSet<Match>();
        }

        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public bool IsLive { get; set; }
        public int? CategoryID { get; set; }
        public int? SportID { get; set; }

        [ForeignKey("SportID")]
        [InverseProperty("Events")]
        public virtual Sport Sport { get; set; }
        [InverseProperty("Event")]
        public virtual ICollection<Match> Matches { get; set; }
    }
}