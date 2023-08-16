#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltraPlay_evaluation.Data.Entities
{
    public partial class Event : IBaseEntity
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
        [NotMapped]
        public virtual ICollection<Match> Matches { get; set; }
    }
}