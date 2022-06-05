using System;

namespace ChessMate.Infrastructure.Entities
{
    public class PositionEntity : BaseEntity
    {
        public int FigureID { get; set; }

        public FigureEntity Figure { get; set; }

        public int ColorID { get; set; }

        public ColorEntity Color { get; set; }

        public string PreviousPosition { get; set; }

        public string CurrentPosition { get; set; }

        public DateTimeOffset InsertDate { get; set; }
    }
}
