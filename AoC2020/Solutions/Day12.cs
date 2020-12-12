using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day12 : Solution<int>
    {
        public List<Instruction> instructions;
        public (int x, int y) position;
        public Direction direction = Direction.East;
        public Day12(string file) : base(file)
        {
            instructions = Input.Select(i => new Instruction
            {
                Action = i.First(char.IsLetter),
                Value = int.Parse(i.Where(char.IsDigit).ToArray( ))
            }).ToList( );
        }

        public override int SolvePart1( )
        {
            instructions.ForEach(i =>
            {
                if ( i.Action == 'F' )
                    position = MoveForward(i);
                if ( i.Action == 'L' || i.Action == 'R' )
                    direction = ChangeDirection(i);
                if ( i.Action == 'N' || i.Action == 'E' ||
                     i.Action == 'S' || i.Action == 'W' )
                    position = MoveAbsolute(i);
            });

            return Math.Abs(position.x) + Math.Abs(position.y);
        }

        public override int SolvePart2( )
        {
            var wayPoint = (x: 10, y: 1);
            position = (0, 0);
            instructions.ForEach(i =>
            {
                if ( i.Action == 'N' )
                    wayPoint = (wayPoint.x, wayPoint.y + i.Value);
                if ( i.Action == 'S' )
                    wayPoint = (wayPoint.x, wayPoint.y - i.Value);
                if ( i.Action == 'E' )
                    wayPoint = (wayPoint.x + i.Value, wayPoint.y);
                if ( i.Action == 'W' )
                    wayPoint = (wayPoint.x - i.Value, wayPoint.y);

                if ( (i.Action == 'L' && i.Value == 90 ) || ( i.Action == 'R' && i.Value == 270 ) )
                    wayPoint = (-wayPoint.y, wayPoint.x);

                if ( ( i.Action == 'R' && i.Value == 90 ) || ( i.Action == 'L' && i.Value == 270 ) )
                    wayPoint = (wayPoint.y, -wayPoint.x);

                if((i.Action == 'R' || i.Action == 'L') && i.Value == 180)
                    wayPoint = (-wayPoint.x, -wayPoint.y);

                if ( i.Action == 'F' )
                    position = (position.x + ( wayPoint.x * i.Value ), position.y + ( wayPoint.y * i.Value ));

            });

            return Math.Abs(position.x) + Math.Abs(position.y);
        }


        public (int x, int y) MoveForward(Instruction ins)
        {
            return direction switch
            {
                Direction.North => (position.x, position.y + ins.Value),
                Direction.East => (position.x + ins.Value, position.y),
                Direction.South => (position.x, position.y - ins.Value),
                Direction.West => (position.x - ins.Value, position.y),
            };
        }

        public (int x, int y) MoveAbsolute(Instruction ins)
        {
            return ins.Action switch
            {
                'N' => (position.x, position.y + ins.Value),
                'E' => (position.x + ins.Value, position.y),
                'S' => (position.x, position.y - ins.Value),
                'W' => (position.x - ins.Value, position.y),
            };
        }

        public Direction ChangeDirection(Instruction ins)
        {
            return ins.Action switch
            {
                'L' or 'R' when direction == Direction.North && ins.Value == 180 => Direction.South,
                'L' or 'R' when direction == Direction.East && ins.Value == 180 => Direction.West,
                'L' or 'R' when direction == Direction.South && ins.Value == 180 => Direction.North,
                'L' or 'R' when direction == Direction.West && ins.Value == 180 => Direction.East,

                'L' when direction == Direction.North && ins.Value == 90 => Direction.West,
                'L' when direction == Direction.North && ins.Value == 270 => Direction.East,
                'R' when direction == Direction.North && ins.Value == 90 => Direction.East,
                'R' when direction == Direction.North && ins.Value == 270 => Direction.West,

                'L' when direction == Direction.East && ins.Value == 90 => Direction.North,
                'L' when direction == Direction.East && ins.Value == 270 => Direction.South,
                'R' when direction == Direction.East && ins.Value == 90 => Direction.South,
                'R' when direction == Direction.East && ins.Value == 270 => Direction.North,

                'L' when direction == Direction.South && ins.Value == 90 => Direction.East,
                'L' when direction == Direction.South && ins.Value == 270 => Direction.West,
                'R' when direction == Direction.South && ins.Value == 90 => Direction.West,
                'R' when direction == Direction.South && ins.Value == 270 => Direction.East,

                'L' when direction == Direction.West && ins.Value == 90 => Direction.South,
                'L' when direction == Direction.West && ins.Value == 270 => Direction.North,
                'R' when direction == Direction.West && ins.Value == 90 => Direction.North,
                'R' when direction == Direction.West && ins.Value == 270 => Direction.South,
            };
        }

        public enum Direction
        {
            North = 0,
            East = 90,
            South = 180,
            West = 270
        }

        public record Instruction
        {
            public char Action;
            public int Value;
        }
    }   
}
