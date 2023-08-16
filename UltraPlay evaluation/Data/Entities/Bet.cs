#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltraPlay_evaluation.Data.Entities
{
    public partial class Bet : IBaseEntity
    {
        public Bet()
        {
            Odds = new HashSet<Odd>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public bool IsLive { get; set; }
        public int? MatchID { get; set; }

        [ForeignKey("MatchID")]
        [InverseProperty("Bets")]
        public virtual Match Match { get; set; }
        [InverseProperty("Bet")]
        public virtual ICollection<Odd> Odds { get; set; }
    }
}