namespace AoC2019;

public class Day02(string file) : Solution(file)
{
	public override async Task<string> SolvePart1()
	{
		var pc = new IntCodeComputer(Input);

		pc.Memory[1] = 12;
		pc.Memory[2] = 2;

		pc.Execute( );

		return pc.Memory[0].ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var noun = 0;
		var verb = 0;

		while (true)
		{
			var pc = new IntCodeComputer(Input);

			pc.Memory[1] = noun;
			pc.Memory[2] = verb;
			pc.Execute();

			if (pc.Memory[0] == 19690720)
				break;

			noun++;

			if (noun == 100)
			{
				noun = 0;
				verb++;
			}

		}

		return ((100 * noun) + verb).ToString( );
		}
	}