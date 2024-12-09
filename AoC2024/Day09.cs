namespace AoC2024;

public class Day09 : Solution
{
	private readonly string diskMap;
	public Day09(string file) : base(file) => diskMap = Input.First( );

	public override async Task<string> SolvePart1()
	{
		var pointer = 0;
		var checkSum = 0L;
		var idFront = 0;
		var idBack = diskMap.Length / 2;
		var idxBack = diskMap.Length - 1;
		var remaining = diskMap[idxBack].ToInt( );
		
		for (var idx = 0 ;idx < diskMap.Length - 1 ;idx++)
		{
			var sizeFront = diskMap[idx].ToInt( );

			if (int.IsOddInteger(idx))
			{
				for (var i = 0 ;i < sizeFront ;i++)
				{
					checkSum += (pointer * idBack);
					pointer++;
					remaining--;
					if (remaining == 0)
					{
						idBack--;
						idxBack -= 2;
						remaining = diskMap[idxBack].ToInt( );
					}
				}
				continue;
			}

			if (idFront > idBack) break;

			if (idFront == idBack)
			{
				for (var i = 0 ;i < remaining ;i++)
				{
					checkSum += (pointer * idFront);
					pointer++;
				}

				break;
			}

			for (var i = 0 ;i < sizeFront ;i++)
			{
				checkSum += (pointer * idFront);
				pointer++;
			}

			idFront++;
		}


		return checkSum.ToString();
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}
}
