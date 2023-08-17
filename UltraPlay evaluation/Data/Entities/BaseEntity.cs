using System.ComponentModel.DataAnnotations;

namespace UltraPlay_evaluation.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
