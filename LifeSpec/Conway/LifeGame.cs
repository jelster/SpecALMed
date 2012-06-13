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
        private List<Action> changesToApply = new List<Action>();
        public IEnumerable<Cell> Cells { get { return grid.OrderBy(x => x.Key.Item1).ThenBy(x => x.Key.Item2).Select(x => x.Value); } }
 
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
            changesToApply.Clear();
            foreach (var cell in grid)
            {
                KeyValuePair<Tuple<int, int>, Cell> closureCell = cell;
                var neighbors = GetNeighbors(closureCell);

                var liveCount = neighbors.Count(x => x.State == Cell.CellState.Alive);

                if ((liveCount > 3 || liveCount < 2))
                {
                    changesToApply.Add(() => closureCell.Value.SetState(Cell.CellState.Dead));
                    
                }
                if (liveCount == 3 || liveCount == 2)
                {
                    changesToApply.Add(() => closureCell.Value.SetState(Cell.CellState.Alive));
                }
            }
            changesToApply.ForEach(x => x());
        }

        public List<Cell> GetNeighbors(KeyValuePair<Tuple<int, int>, Cell> closureCell)
        {
            return grid.DefaultIfEmpty().Where(x => x.Key != closureCell.Key)
                .Where(x => Math.Abs(closureCell.Key.Item1 - x.Key.Item1) <= 1 && Math.Abs(closureCell.Key.Item2 - x.Key.Item2) <= 1)
                .Select(x => x.Value)
                .ToList();
        }

        public Cell GetCell(int xPos, int yPos)
        {
            return grid[Tuple.Create(xPos, yPos)];
        }
    }
}