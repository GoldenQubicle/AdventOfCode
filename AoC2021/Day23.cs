namespace AoC2021;

public class Day23 : Solution
{
	public Day23(string file) : base(file) { }

	//solved both parts manually. 

	public override async Task<string> SolvePart1()
	{
		return "15472";
	}

	public override async Task<string> SolvePart2()
	{
		return "46182";
	}

	/*

    How to even approach this in code...?
    There's four rooms which each contain 2 spots which can be empty or occupied
    There's a hallway with a number of spots which can be empty or occupied
    There's a limited number of moves, i.e. I could precompute all possible moves

    moves from room <-> hallway can be computed with manhattan distance

	strategy...?  Since I solved both manually, it seems start with clearing out room D works for both parts. 

    amphipod
    - type, A, B, C or D
    - when in destination room, does no longer need to be considered for movement
    - when in starting room can either move to destination room or hallway
    - when in hallway can only move to destination room
    - location
	    - start room / destination room
	    - hallway
    - tryMove
	    -

    - canMove
	    - in room
		    - the destination room has a spot and clear path -> move to destination room
		    - the destination room has a spot but the path is blocked -> queue state to clear blockade (?)


	    - in hallway
		    - the destination room has a spot and clear path -> move to destination room
		    - the destination room has a spot but the path is blocked -> queue state to clear blockade (?)
		    - the destination room doesn't have a spot

    room
    - type, A, B, C or D
    - has 2 slots
    - has a spot -> is empty or contains amphipod of same type

    hallway
    - has 7 slots
    - has 4 entrances

*/


}
