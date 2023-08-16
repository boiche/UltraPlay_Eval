#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltraPlay_evaluation.Data.Entities
{
    public partial class Sport : IBaseEntity
    {
        public Sport()
        {
            Events = new HashSet<Event>();
        }

        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [InverseProperty("Sport")]
        [NotMapped]
        public virtual ICollection<Event> Events { get; set; }
    }
}