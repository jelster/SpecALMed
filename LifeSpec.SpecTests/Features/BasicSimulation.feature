Feature: Basic Simulation
	In order to explore the principles of computational science
	As a dummy
	I want to run a simulation of Conway's Game Of Life
	so that I can stop being a dummy


Scenario: Live cell dies 
	Given A grid 60 wide by 60 high
	And a live cell at x15 y30
	When the simulation is advanced 1 step
	Then the cell at x15 y30 should be dead
