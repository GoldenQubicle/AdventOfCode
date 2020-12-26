using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AoC2020Tests
{
    class AllDaysResultsTest
    {
        [SetUp]
        public void Setup( )
        {
            var results = File.ReadAllLines($"{Directory.GetCurrentDirectory( )}/data/AllResults.txt")
                .Select((line, i) => (line.Split("part"), i))
                .Aggregate(new Dictionary<string, (string pt1, string pt2)>( ), (dic, line) =>
                 {
                     var day = line.Item1[0].Replace(" ", "");
                     if ( line.i % 2 == 0 )
                         dic.Add(day, (line.Item1[1][4..], string.Empty));
                     else
                         dic[day] = (dic[day].pt1, line.Item1[1][4..]);
                     return dic;
                 });

            var assembly = Assembly.LoadFrom($"{Directory.GetCurrentDirectory( )}/AoC2020.dll");
            var dayType = assembly.GetType("AoC2020.Day01");
            var ctorType = new Type[ ] { typeof(string) };
            var ctor = dayType.GetConstructor(ctorType);
            var newDay = ( Solution ) ctor.Invoke(new object[ ] { "Day01" });

            var actualPt1 = newDay.SolvePart1( );
            var actualPt2 = newDay.SolvePart2( );

            Assert.AreEqual(results["Day01"].pt1, actualPt1);
            Assert.AreEqual(results["Day01"].pt2, actualPt2);
        }

        [Test]
        public void AllDays( )
        {

        }

    }
}
