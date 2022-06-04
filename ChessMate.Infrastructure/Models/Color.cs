using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Infrastructure.Models
{
    public class Color
    {
        [Key]
        public int ID { get; set; }

        public string Description { get; set; }
    }
}
