using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace LifeSpec.SpecTests.Steps
{
    [Binding]
    public class LifeSteps
    {
        [Given(@"A grid (\d+) wide by (\d+) high")]
        public void Given_a_grid(int width, int height)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"a live cell at x(\d+) y(\d+)")]
        public void Given_a_cell(int xPos, int yPos)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"the simulation is advanced (\d+) step")]
        public void When_the_simulation_is_advanced(int stepsToAdvance)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the cell at x(\d+) y(\d+) should be dead")]
        public void Then_the_cell_should_be_dead(int xPos, int yPos)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
