using System.ComponentModel.DataAnnotations;

namespace ChessMate.Infrastructure.Entities
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
