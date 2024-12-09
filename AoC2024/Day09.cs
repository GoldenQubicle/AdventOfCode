using System.Reflection;

namespace AoC2024;

public class Day09 : Solution
{
	private string diskMap;
	public Day09(string file) : base(file) => diskMap = Input.First( );

	public override async Task<string> SolvePart1()
	{
		var pointer = 0;
		var checkSum = 0L;
		var frontValue = 0;
		var backValue = diskMap.Length / 2;
		var backIdx = diskMap.Length - 1;
		var backBlocks = diskMap[backIdx].ToInt( );


		for (var idx = 0 ;idx < diskMap.Length - 1 ;idx++)
		{
			var frontBlocks = diskMap[idx].ToInt( );

			if (int.IsOddInteger(idx)) // the front block is free, start moving the back blocks
			{
				for (var i = 0 ;i < frontBlocks ;i++)
				{
					checkSum += pointer * backValue;
					pointer++;
					backBlocks--;

					if (backBlocks == 0) // we still have free front blocks but out of back blocks
					{
						backValue--;
						backIdx -= 2;
						backBlocks = diskMap[backIdx].ToInt( );
					}
				}
				continue;
			}

			if (frontValue > backValue)
				break; //don't overshot

			if (frontValue == backValue)
			{
				//at the midpoint, we may already have moved some of the back blocks
				//just gotta account for the remainder of the back block
				for (var i = 0 ;i < backBlocks ;i++)
				{
					checkSum += pointer * frontValue;
					pointer++;
				}

				break; //all that's left is empty 
			}

			//add the front blocks to the checksum
			for (var i = 0 ;i < frontBlocks ;i++)
			{
				checkSum += pointer * frontValue;
				pointer++;
			}

			frontValue++;
		}


		return checkSum.ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var movedFiles = new Dictionary<int, List<int>>( );
		var freeSpaces = diskMap
			.WithIndex( )
			.Where(t => int.IsOddInteger(t.idx))
			.ToDictionary(t => t.idx, t => t.Value.ToInt( ));

		var backValue = diskMap.Length / 2;
		var backIdx = diskMap.Length - 1;

		//move the files
		while (backIdx > 0)
		{
			var size = diskMap[backIdx].ToInt( );
			var index = freeSpaces.FirstOrDefault(kvp => kvp.Value >= size && kvp.Key < backIdx);

			if (index is { Key: 0, Value: 0 }) //not enough free space available, work backwards over the files
			{
				backIdx -= 2; 
				backValue--;
				continue;
			}

			// keep track of moved files to properly calculate the checksum later
			if (!movedFiles.TryAdd(index.Key, Enumerable.Repeat(backValue, size).ToList( )))
				movedFiles[index.Key].AddRange(Enumerable.Repeat(backValue, size));

			// account for space taken up by moved files to properly calculate the checksum later
			freeSpaces[index.Key] -= size; 

			// account for the freed up space to properly calculate the checksum later
			freeSpaces.Add(backIdx, size);

			//work backwards
			backIdx -= 2;
			backValue--;
		}

		var pointer = 0;
		var idFront = 0;
		var checkSum = 0L;

		//calculate the checksum
		for (var idx = 0 ;idx < diskMap.Length ;idx++)
		{
			if (int.IsOddInteger(idx)) // formerly free space but could be we moved files so check both
			{
				if (movedFiles.TryGetValue(idx, out var values))
				{
					values.ForEach(v =>
					{
						checkSum += pointer * v;
						pointer++;
					});
				}

				if (freeSpaces.TryGetValue(idx, out var free1))
					pointer += free1;

				continue;
			}

			//formerly occupied but could be moved and thus freed up
			if (freeSpaces.TryGetValue(idx, out var free2))
				pointer += free2;
			else
			{
				var size = diskMap[idx].ToInt( );
				for (var i = 0 ;i < size ;i++)
				{
					checkSum += pointer * idFront;
					pointer++;
				}
			}


			idFront++;
		}

		return checkSum.ToString( );
	}
}
