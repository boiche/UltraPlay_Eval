﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltraPlay_evaluation.Data.Entities
{
    public partial class GetPreviewBetsResult
    {
        public int OddID { get; set; }
        public string OddName { get; set; }
        public decimal? SpecialBetValue { get; set; }
        public decimal OddValue { get; set; }
        public bool OddIsActive { get; set; }
        public int BetID { get; set; }
        public string BetName { get; set; }
        public bool BetIsActive { get; set; }
        public bool IsLive { get; set; }
        public int? MatchID { get; set; }
    }
}