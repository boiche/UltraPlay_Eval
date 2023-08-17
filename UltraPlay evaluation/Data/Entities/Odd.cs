#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UltraPlay_evaluation.Data.Entities
{
    public partial class Odd : BaseEntity
    {
        [StringLength(250)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Value { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? SpecialBetValue { get; set; }
        public int? BetID { get; set; }

        [ForeignKey("BetID")]
        [InverseProperty("Odds")]
        [JsonIgnore]
        public virtual Bet Bet { get; set; }
    }
}