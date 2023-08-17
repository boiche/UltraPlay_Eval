#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UltraPlay_evaluation.Data.Entities
{
    public partial class Bet : BaseEntity
    {
        public Bet()
        {
            Odds = new HashSet<Odd>();
        }

        [StringLength(250)]
        public string Name { get; set; }
        public bool IsLive { get; set; }
        public int? MatchID { get; set; }

        [ForeignKey("MatchID")]
        [InverseProperty("Bets")]
        [JsonIgnore]
        public virtual Match Match { get; set; }
        [InverseProperty("Bet")]
        public virtual ICollection<Odd> Odds { get; set; }
    }
}