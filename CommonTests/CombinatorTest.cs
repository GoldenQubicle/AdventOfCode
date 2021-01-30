using Common;
using NUnit.Framework;
using System.Collections.Generic;


namespace CommonTests
{
    public class CombinatorTest
    {

        public static List<CombinationTestDate> CombinationData = new List<CombinationTestDate>
       {
            new CombinationTestDate
            {
                Clength = 3,
                Elements = new List<int>{ 4, 5, 6},
                Expected = new List<List<int>>
                {
                    new List<int> { 4, 4, 4},
                    new List<int> { 5, 4, 4},
                    new List<int> { 6, 4, 4},
                    new List<int> { 4, 5, 4},
                    new List<int> { 5, 5, 4},
                    new List<int> { 6, 5, 4},
                    new List<int> { 4, 6, 4},
                    new List<int> { 5, 6, 4},
                    new List<int> { 6, 6, 4},

                    new List<int> { 4, 4, 5},
                    new List<int> { 5, 4, 5},
                    new List<int> { 6, 4, 5},
                    new List<int> { 4, 5, 5},
                    new List<int> { 5, 5, 5},
                    new List<int> { 6, 5, 5},
                    new List<int> { 4, 6, 5},
                    new List<int> { 5, 6, 5},
                    new List<int> { 6, 6, 5},

                    new List<int> { 4, 4, 6},
                    new List<int> { 5, 4, 6},
                    new List<int> { 6, 4, 6},
                    new List<int> { 4, 5, 6},
                    new List<int> { 5, 5, 6},
                    new List<int> { 6, 5, 6},
                    new List<int> { 4, 6, 6},
                    new List<int> { 5, 6, 6},
                    new List<int> { 6, 6, 6},
                }
            },
            new CombinationTestDate
            {
                Clength = 2,
                Elements = new List<int>{ 8, 12, 52 },
                Expected = new List<List<int>>
                {
                    new List<int>{8,8},
                    new List<int>{12,8},
                    new List<int>{52,8},
                    new List<int>{8,12},
                    new List<int>{12,12},
                    new List<int>{52,12},
                    new List<int>{8,52},
                    new List<int>{12,52},
                    new List<int>{52,52},
                }
            },
           new CombinationTestDate
           {
               Clength = 2,
               Elements = new List<int>{ 0, 1 },
               Expected = new List<List<int>>
               {
                   new List<int>{0,0},
                   new List<int>{1,0},
                   new List<int>{0,1},
                   new List<int>{1,1},
               }
           },
       };

        [TestCaseSource(nameof(CombinationData))]
        public void CombinatorIntegerTest(CombinationTestDate testData)
        {
            var actual = Combinator.Generate(testData.Elements, testData.Clength);

            Assert.AreEqual(testData.Expected, actual);
        }

        public record CombinationTestDate
        {
            public int Clength { get; init; }
            public List<int> Elements { get; init; }
            public List<List<int>> Expected { get; init; }
        };
    }
}