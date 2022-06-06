namespace ChessMate.Models.Models
{
    public class Position
    {
        public int Figure { get; set; }

        public int Color { get; set; }

        public string PreviousPosition { get; set; }

        public string CurrentPosition { get; set; }
    }
}
