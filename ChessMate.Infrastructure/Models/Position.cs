using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Infrastructure.Models
{
    public class Position
    {
        [Key]
        public int ID { get; set; }

        public Figure Figure { get; set; }

        public Color Color { get; set; }

        public string PreviousPosition { get; set; }

        public string CurrentPosition { get; set; }

        public DateTimeOffset InsertDate { get; set; }
    }
}
