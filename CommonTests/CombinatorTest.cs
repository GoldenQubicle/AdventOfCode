using Common;
using NUnit.Framework;
using System.Collections.Generic;

namespace CommonTests
{
    public class CombinatorTest
    {
        [TestCaseSource(nameof(CombinationTestDataString))]
        public void CombinatorStringTest(CombinationTestData<string> data)
        {
            var actual = Combinator.Generate(data.Elements, data.isFullSet);

            Assert.AreEqual(data.Expected, actual);
        }

        [TestCaseSource(nameof(CombinationTestDataIntegers))]
        public void CombinatorIntegerTest(CombinationTestData<int> data)
        {
            var actual = Combinator.Generate(data.Elements, data.Clength);

            Assert.AreEqual(data.Expected, actual);
        }

        public record CombinationTestData<T>
        {
            public T Clength { get; init; }
            public List<T> Elements { get; init; }
            public List<List<T>> Expected { get; init; }
            public bool isFullSet { get; init; }
        };

        public static List<CombinationTestData<int>> CombinationTestDataIntegers = new List<CombinationTestData<int>>
        {
             new CombinationTestData<int>
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
             new CombinationTestData<int>
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
        };

        public static List<CombinationTestData<string>> CombinationTestDataString = new List<CombinationTestData<string>>
        {
            new CombinationTestData<string>
            {
                Elements = new List<string>
                {
                    "A", "b", "7"
                },
                isFullSet = true,
                Expected =  new List<List<string>>
                {
                    new List<string>{"A", "A"},
                    new List<string>{"b", "A"},
                    new List<string>{"7", "A"},
                    new List<string>{"A", "b"},
                    new List<string>{"b", "b"},
                    new List<string>{"7", "b"},
                    new List<string>{"A", "7"},
                    new List<string>{"b", "7"},
                    new List<string>{"7", "7"},

                    new List<string>{"A", "A", "A" },
                    new List<string>{"b", "A", "A" },
                    new List<string>{"7", "A", "A" },
                    new List<string>{"A", "b", "A" },
                    new List<string>{"b", "b", "A" },
                    new List<string>{"7", "b", "A" },
                    new List<string>{"A", "7", "A" },
                    new List<string>{"b", "7", "A" },
                    new List<string>{"7", "7", "A" },

                    new List<string>{"A", "A", "b"},
                    new List<string>{"b", "A", "b"},
                    new List<string>{"7", "A", "b"},
                    new List<string>{"A", "b", "b"},
                    new List<string>{"b", "b", "b"},
                    new List<string>{"7", "b", "b"},
                    new List<string>{"A", "7", "b"},
                    new List<string>{"b", "7", "b"},
                    new List<string>{"7", "7", "b"},

                    new List<string>{"A", "A", "7"},
                    new List<string>{"b", "A", "7"},
                    new List<string>{"7", "A", "7"},
                    new List<string>{"A", "b", "7"},
                    new List<string>{"b", "b", "7"},
                    new List<string>{"7", "b", "7"},
                    new List<string>{"A", "7", "7"},
                    new List<string>{"b", "7", "7"},
                    new List<string>{"7", "7", "7"},
                }
            },
             new CombinationTestData<string>
            {
                Elements = new List<string>
                {
                    "A", "b", "7"
                },
                isFullSet = false,
                Expected =  new List<List<string>>
                {
                    new List<string>{"A", "A", "A" },
                    new List<string>{"b", "A", "A" },
                    new List<string>{"7", "A", "A" },
                    new List<string>{"A", "b", "A" },
                    new List<string>{"b", "b", "A" },
                    new List<string>{"7", "b", "A" },
                    new List<string>{"A", "7", "A" },
                    new List<string>{"b", "7", "A" },
                    new List<string>{"7", "7", "A" },

                    new List<string>{"A", "A", "b"},
                    new List<string>{"b", "A", "b"},
                    new List<string>{"7", "A", "b"},
                    new List<string>{"A", "b", "b"},
                    new List<string>{"b", "b", "b"},
                    new List<string>{"7", "b", "b"},
                    new List<string>{"A", "7", "b"},
                    new List<string>{"b", "7", "b"},
                    new List<string>{"7", "7", "b"},

                    new List<string>{"A", "A", "7"},
                    new List<string>{"b", "A", "7"},
                    new List<string>{"7", "A", "7"},
                    new List<string>{"A", "b", "7"},
                    new List<string>{"b", "b", "7"},
                    new List<string>{"7", "b", "7"},
                    new List<string>{"A", "7", "7"},
                    new List<string>{"b", "7", "7"},
                    new List<string>{"7", "7", "7"},
                }
            },

        };

    }
}