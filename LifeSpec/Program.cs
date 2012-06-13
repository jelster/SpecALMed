using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LifeSpec.Conway;

namespace LifeSpec
{
    class Program
    {
        static void Main(string[] args)
        {
            int w, h, c;
            if (args.Any() && args.Count() == 2)
            {
                int.TryParse(args[0], out w);
                int.TryParse(args[1], out h);
            }
            else
            {
                w = 60;
                h = 60;
            }

            int.TryParse(args.ElementAtOrDefault(2) ?? "45", out c);

            var game = new LifeGame(w, h);
            Console.SetWindowSize(w, h);
            Console.SetWindowPosition(0, 0);
            Console.SetBufferSize(w, h);
            Console.WriteLine("Created {0} x {1} grid", w, h);

            Console.WriteLine("Enter location of a live cell in the format [x,y] and press Enter.");
            Console.WriteLine("Leave blank to finish entering initial  Ex: 2,5");
            Console.WriteLine("No entries will cause initial state to be randomly determined");

            bool done = false;
            while (!done)
            {
                var val = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(val))
                {
                    done = true;
                    continue;
                }
                var tupe = val.Split(',');
                int x, y;
                int.TryParse(tupe.ElementAtOrDefault(0), out x);
                int.TryParse(tupe.ElementAtOrDefault(1), out y);


                var newCell = new Cell(x, y, Cell.CellState.Alive);
                if (game.Cells.Any(p => p.Position == newCell.Position)) continue;

                game.SpawnCell(newCell);

            }

            if (!game.Cells.Any())
            {
                var rng = new Random(DateTime.Now.Millisecond);
                var randCountGoal = rng.Next(2, w * h - 1);
                while (game.Cells.Count() < randCountGoal)
                {
                    var randCell = new Cell(rng.Next(0, w), rng.Next(0, h), rng.NextDouble() > 0.15 ? Cell.CellState.Alive : Cell.CellState.Dead);

                    if (!game.Cells.Select(x => x.Position).Contains(randCell.Position)) 
                        game.SpawnCell(randCell);
                }
            }

            Console.Clear();
            var oldColor = Console.ForegroundColor;
            Console.CursorVisible = false;
            
            for (int i = 0; i < c; i++)
            {
                game.Step(1);
                var cells = game.Cells;
                DrawGrid(cells);
                Thread.Sleep(33);
            }
            Console.WriteLine();
            Console.ForegroundColor = oldColor;
            Console.ReadKey();
        }

        private static void DrawGrid(IEnumerable<Cell> game)
        {
            Console.Clear();
            
            foreach (var cell in game)
            {
                Console.SetCursorPosition(cell.Position.Item1, cell.Position.Item2);
                if (cell.State == Cell.CellState.Alive) 
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                else
                    Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write("*");
            }
        }
    }
}
