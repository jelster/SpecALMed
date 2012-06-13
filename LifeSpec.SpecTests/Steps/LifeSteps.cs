using System.Collections.Generic;
using System.Linq;
using System.Text;
using LifeSpec.Conway;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace LifeSpec.SpecTests.Steps
{
    [Binding]
    public class LifeSteps
    {
        private Cell cell;

        private LifeGame lifeGame;

        [Given(@"A grid (\d+) wide by (\d+) high")]
        public void Given_a_grid(int width, int height)
        {
            lifeGame = new LifeGame(width, height);
        }

        [Given(@"a live cell at x(\d+) y(\d+)")]
        public void Given_a_cell(int xPos, int yPos)
        {
            cell = new Cell(xPos, yPos, Cell.CellState.Alive);
            lifeGame.SpawnCell(cell);
        }

        [When(@"the simulation is advanced (\d+) step")]
        public void When_the_simulation_is_advanced(int stepsToAdvance)
        {
            lifeGame.Step(stepsToAdvance);
        }

        [Then(@"the cell at x(\d+) y(\d+) should be dead")]
        public void Then_the_cell_should_be_dead(int xPos, int yPos)
        {
            Assert.IsTrue(cell.State == Cell.CellState.Dead);
        }
    }
}