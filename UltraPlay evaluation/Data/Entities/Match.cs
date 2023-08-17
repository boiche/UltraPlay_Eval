#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltraPlay_evaluation.Data.Entities
{
    public partial class Match : BaseEntity
    {
        public Match()
        {
            Bets = new HashSet<Bet>();
        }

        [StringLength(250)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        public int? EventID { get; set; }
        public string MatchType { get; set; }

        [ForeignKey("EventID")]
        [InverseProperty("Matches")]
        public virtual Event Event { get; set; }        
        [InverseProperty("Match")]
        public virtual ICollection<Bet> Bets { get; set; }
    }
}