Feature: Basic Simulation
	In order to explore the principles of computational science
	As a dummy
	I want to run a simulation of Conway's Game Of Life
	so that I can stop being a dummy

Scenario: Live cell dies of loneliness
	Given a grid 60 wide by 60 high
	And an alive cell at x15 y30
	When the simulation is advanced 1 step
	Then the cell at x15 y30 should be dead

Scenario: Live cell dies of starvation
	Given a grid 15 wide by 15 high
	And live cells at the following positions:
	| x | y |
	| 4 | 6 |
	| 5 | 5 |
	| 5 | 6 |
	| 6 | 5 |
	| 4 | 5 |
	When the simulation is advanced 1 step
	Then the cell at x5 y5 should be dead

Scenario: Live cell lives in contentment with partner
	Given a grid 60 wide by 60 high
	And an alive cell at x15 y30
	And an alive cell at x16 y30
	And an alive cell at x14 y30
	When the simulation is advanced 1 step
	Then the cell at x15 y30 should be alive

Scenario: Lazarus rising
	Given a grid 12 wide by 12 high
	And a dead cell at x5 y5
	And live cells at the following positions:
	| x | y |
	| 4 | 5 |
	| 6 | 5 |
	| 5 | 6 |
	When the simulation is advanced 1 step
	Then the cell at x5 y5 should be alive


