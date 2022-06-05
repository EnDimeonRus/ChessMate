using System;

namespace ChessMate.Infrastructure.Models
{
    public class PositionEntity : BaseEntity
    {
        public FigureEntity Figure { get; set; }

        public ColorEntity Color { get; set; }

        public string PreviousPosition { get; set; }

        public string CurrentPosition { get; set; }

        public DateTimeOffset InsertDate { get; set; }
    }
}
