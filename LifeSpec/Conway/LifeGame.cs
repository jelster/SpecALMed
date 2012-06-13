using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeSpec.Conway
{
    public class LifeGame
    {
        public int Height { get; private set; }
        public int Width { get; private set; }

        private readonly Dictionary<Tuple<int, int>, Cell> grid;

        public LifeGame(int width, int height)
        {
            Width = width;
            Height = height;
            grid = new Dictionary<Tuple<int, int>, Cell>(width*height+1);
        }

        public void SpawnCell(Tuple<int, int> location, Cell.CellState initialState = Cell.CellState.Dead)
        {
            SpawnCell(new Cell(location.Item1, location.Item2, initialState));
        }
        public void Step(int stepsToAdvance)
        {
            for (int i = 0; i < stepsToAdvance; i++)
            {
                Step();
            }
        }

        public void SpawnCell(Cell cell)
        {
            grid.Add(cell.Position, cell);
        }

        private void Step()
        {
            foreach (var cell in grid)
            {
                KeyValuePair<Tuple<int, int>, Cell> closureCell = cell;
                var neighbors = grid.DefaultIfEmpty()
                    .Where(x => Math.Abs(closureCell.Key.Item1 - x.Key.Item1) == 1 && Math.Abs(closureCell.Key.Item2 - x.Key.Item2) == 1)
                    .ToList();

                var liveCount = neighbors.Sum(x => (int)x.Value.State);

                if ((liveCount > 3 || liveCount < 2) || !neighbors.Any())
                {
                    cell.Value.SetState(Cell.CellState.Dead);
                }
            }
        }
    }
}