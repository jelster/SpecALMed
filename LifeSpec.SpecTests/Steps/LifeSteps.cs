using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LifeSpec.Conway;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace LifeSpec.SpecTests.Steps
{
    [Binding]
    public class LifeSteps
    {
        private LifeGame lifeGame;

        [Given(@"[Aa] grid (\d+) wide by (\d+) high")]
        public void Given_a_grid(int width, int height)
        {
            lifeGame = new LifeGame(width, height);
        }

        [Given(@"[a|an]+ (dead|alive) cell at x(\d+) y(\d+)")]
        public void Given_a_cell(Cell.CellState state, int xPos, int yPos)
        {
            lifeGame.SpawnCell(Tuple.Create(xPos, yPos), state);
        }

        [When(@"the simulation is advanced (\d+) step")]
        public void When_the_simulation_is_advanced(int stepsToAdvance)
        {
            lifeGame.Step(stepsToAdvance);
        }

        [Then(@"the cell at x(\d+) y(\d+) should be (dead|alive)")]
        public void Then_the_cell_should_be_dead(int xPos, int yPos, Cell.CellState state)
        {
            var cell = lifeGame.GetCell(xPos, yPos);
            Assert.IsNotNull(cell);
            Assert.IsTrue(cell.State == state);
        }

        [Given(@"live cells at the following positions:")]
        public void GivenLiveCellsAtTheFollowingPositions(IEnumerable<Tuple<int, int>> table)
        {
            table.Select(c => new Cell(c.Item1, c.Item2, Cell.CellState.Alive)).ToList().ForEach(lifeGame.SpawnCell);
        }

        [StepArgumentTransformation("cells at the following positions")]
        public IEnumerable<Tuple<int, int>> TupleTableTransformer(Table table)
        {
            return table.Rows.Select(x => Tuple.Create(x.GetInt32("x"), x.GetInt32("y")));
        }

        //[StepArgumentTransformation]
        //public bool AliveOrDeadTransformer(string state)
        //{
            
        //}

    }
}