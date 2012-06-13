using System;

namespace LifeSpec.Conway
{
    public class Cell
    {
        public enum CellState
        {
            Dead = 0x0,
            Alive = 0x1
        }

        public Tuple<int, int> Position { get; private set; }
        public CellState State { get; set; }

        public Cell(int xPos, int yPos, CellState cellState)
        {
            Position = Tuple.Create(xPos, yPos);
            State = cellState;
        }
    }
}