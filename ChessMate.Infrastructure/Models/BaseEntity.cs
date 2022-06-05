using System.ComponentModel.DataAnnotations;

namespace ChessMate.Infrastructure.Models
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
