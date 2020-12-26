using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2020
{
    public class Day17 : Solution<int>
    {
        private Dictionary<(int x, int y, int z), char> grid3d = new( );
        private Dictionary<(int x, int y, int z, int w), char> grid4d = new( );

        public Day17(string file) : base(file)
        {
            grid3d = Input.SelectMany((s, y) => s.Select((c, x) => (x, y, z: 0, c)))
                .ToDictionary(i => (i.x, i.y, i.z), i => i.c);

            var n3d = grid3d.SelectMany(c => GetNeighbors3D(c.Key)).Distinct().ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            grid3d.TryAddAll3D(n3d);

            grid4d = Input.SelectMany((s, y) => s.Select((c, x) => (x, y, z: 0, w: 0, c)))
                .ToDictionary(i => (i.x, i.y, i.z, i.w), i => i.c);

            var n4d = grid4d.SelectMany(c => GetNeighbors4D(c.Key)).Distinct( ).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            grid4d.TryAddAll4D(n4d);
        }

        public override int SolvePart1( )
        {
            for ( int i = 0 ; i < 6 ; i++ )
            {
                var newState = new Dictionary<(int x, int y, int z), char>( );
                var expansion = new Dictionary<(int x, int y, int z), char>( );
                foreach ( var (pos, c) in grid3d )
                {
                    var neighbors = GetNeighbors3D(pos);
                    var active = neighbors.Count(n => n.Value == '#');

                    switch ( c )
                    {
                        case '#' when active < 2 || active > 3:
                            newState.Add(pos, '.');
                            break;
                        case '.' when active == 3:
                            newState.Add(pos, '#');
                            break;
                        default:
                            newState.Add(pos, c);
                            break;
                    }

                    expansion.TryAddAll3D(neighbors);
                }
          

                newState.TryAddAll3D(expansion);
                grid3d = newState;
            }
            return grid3d.Values.Count(c => c == '#');
        }

        public override int SolvePart2( )
        {
            for ( int i = 0 ; i < 6 ; i++ )
            {
                var newState =  new Dictionary<(int x, int y, int z, int w), char>( );
                var expansion = new Dictionary<(int x, int y, int z, int w), char>( );
                foreach ( var (pos, c) in grid4d )
                {
                    var neighbors = GetNeighbors4D(pos);
                    var active = neighbors.Count(n => n.Value == '#');

                    switch ( c )
                    {
                        case '#' when active < 2 || active > 3:
                            newState.Add(pos, '.');
                            break;
                        case '.' when active == 3:
                            newState.Add(pos, '#');
                            break;
                        default:
                            newState.Add(pos, c);
                            break;
                    }

                    expansion.TryAddAll4D(neighbors);
                }

                newState.TryAddAll4D(expansion);
                grid4d = newState;
            }
            return grid4d.Values.Count(c => c == '#');
        }

        public Dictionary<(int x, int y, int z), char> GetNeighbors3D((int x, int y, int z) pos)
        {
            return offsets3d.Select(o =>
            {
                var npos = (x: pos.x + o.x, y: pos.y + o.y, z: pos.z + o.z);
                if ( grid3d.ContainsKey(npos) )
                    return (pos: npos, grid3d[npos]);
                else
                    return (pos: npos, '.');

            }).ToDictionary(r => (r.pos.x, r.pos.y, r.pos.z), r => r.Item2);
        }

        public Dictionary<(int x, int y, int z, int w), char> GetNeighbors4D((int x, int y, int z, int w) pos)
        {
            return offsets4d.Select(o =>
            {
                var npos = (x: pos.x + o.x, y: pos.y + o.y, z: pos.z + o.z, w: pos.w + o.w);
                if ( grid4d.ContainsKey(npos) )
                    return (pos: npos, grid4d[npos]);
                else
                    return (pos: npos, '.');

            }).ToDictionary(r => (r.pos.x, r.pos.y, r.pos.z, r.pos.w), r => r.Item2);
        }

        private List<(int x, int y, int z)> offsets3d = new( )
        {
            (-1, -1, 1),
            (0, -1, 1),
            (1, -1, 1),
            (-1, 0, 1),
            (0, 0, 1),
            (1, 0, 1),
            (-1, 1, 1),
            (0, 1, 1),
            (1, 1, 1),

            (-1, -1, 0),
            (0, -1, 0),
            (1, -1, 0),
            (-1, 0, 0),
            (1, 0, 0),
            (-1, 1, 0),
            (0, 1, 0),
            (1, 1, 0),

            (-1, -1, -1),
            (0, -1, -1),
            (1, -1, -1),
            (-1, 0, -1),
            (0, 0, -1),
            (1, 0, -1),
            (-1, 1, -1),
            (0, 1, -1),
            (1, 1, -1),
        };

        private List<(int x, int y, int z, int w)> offsets4d = new( )
        {
            (-1, -1, 1, 0),
            (0, -1, 1, 0),
            (1, -1, 1, 0),
            (-1, 0, 1, 0),
            (0, 0, 1, 0),
            (1, 0, 1, 0),
            (-1, 1, 1, 0),
            (0, 1, 1, 0),
            (1, 1, 1, 0),
            (-1, -1, 0, 0),
            (0, -1, 0, 0),
            (1, -1, 0, 0),
            (-1, 0, 0, 0),
            (1, 0, 0, 0),
            (-1, 1, 0, 0),
            (0, 1, 0, 0),
            (1, 1, 0, 0),
            (-1, -1, -1, 0),
            (0, -1, -1, 0),
            (1, -1, -1, 0),
            (-1, 0, -1, 0),
            (0, 0, -1, 0),
            (1, 0, -1, 0),
            (-1, 1, -1, 0),
            (0, 1, -1, 0),
            (1, 1, -1, 0),


            (-1, -1, 1, 1),
            (0, -1, 1, 1),
            (1, -1, 1, 1),
            (-1, 0, 1, 1),
            (0, 0, 1, 1),
            (1, 0, 1, 1),
            (-1, 1, 1, 1),
            (0, 1, 1, 1),
            (1, 1, 1, 1),
            (-1, -1, 0, 1),
            (0, -1, 0, 1),
            (1, -1, 0, 1),
            (-1, 0, 0, 1),
            (0, 0, 0, 1),
            (1, 0, 0, 1),
            (-1, 1, 0, 1),
            (0, 1, 0, 1),
            (1, 1, 0, 1),
            (-1, -1, -1, 1),
            (0, -1, -1, 1),
            (1, -1, -1, 1),
            (-1, 0, -1, 1),
            (0, 0, -1, 1),
            (1, 0, -1, 1),
            (-1, 1, -1, 1),
            (0, 1, -1, 1),
            (1, 1, -1, 1),

            (-1, -1, 1, -1),
            (0, -1, 1, -1),
            (1, -1, 1, -1),
            (-1, 0, 1, -1),
            (0, 0, 1, -1),
            (1, 0, 1, -1),
            (-1, 1, 1, -1),
            (0, 1, 1, -1),
            (1, 1, 1, -1),
            (-1, -1, 0, -1),
            (0, -1, 0, -1),
            (1, -1, 0, -1),
            (-1, 0, 0, -1),
            (0, 0, 0, -1),
            (1, 0, 0, -1),
            (-1, 1, 0, -1),
            (0, 1, 0, -1),
            (1, 1, 0, -1),
            (-1, -1, -1, -1),
            (0, -1, -1, -1),
            (1, -1, -1, -1),
            (-1, 0, -1, -1),
            (0, 0, -1, -1),
            (1, 0, -1, -1),
            (-1, 1, -1, -1),
            (0, 1, -1, -1),
            (1, 1, -1, -1),
        };
    }

    public static class Extension
    {
        public static void TryAddAll3D(this Dictionary<(int x, int y, int z), char> dic,
            Dictionary<(int x, int y, int z), char> toAdd)
        {
            toAdd.ForEach3D(kvp => dic.TryAdd(kvp.Key, kvp.Value));
        }

        public static void TryAddAll4D(this Dictionary<(int x, int y, int z, int w), char> dic,
            Dictionary<(int x, int y, int z, int w), char> toAdd)
        {
            toAdd.ForEach4D(kvp => dic.TryAdd(kvp.Key, kvp.Value));
        }

        public static void ForEach3D(this Dictionary<(int x, int y, int z), char> dic,
            Action<KeyValuePair<(int x, int y, int z), char>> action)
        {
            foreach ( var kvp in dic )
            {
                action.Invoke(kvp);
            }
        }
        public static void ForEach4D(this Dictionary<(int x, int y, int z, int w), char> dic,
        Action<KeyValuePair<(int x, int y, int z, int w), char>> action)
        {
            foreach ( var kvp in dic )
            {
                action.Invoke(kvp);
            }
        }
    }
}
