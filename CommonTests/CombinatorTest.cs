using Common;
using NUnit.Framework;
using System.Collections.Generic;

namespace CommonTests
{
    public class CombinatorTest
    {
        [TestCaseSource(nameof(_combinatorTestCases))]
        public void CombinatorTests(CombinatorTestData<string> data)
        {
            var actual = Combinator.Generate(data.Elements, data.Options);
            Assert.AreEqual(data.Expected, actual.Result);
        }

        public record CombinatorTestData<T>
        {
            public T Clength { get; init; }
            public List<T> Elements { get; init; }
            public List<List<T>> Expected { get; init; }
            public CombinatorOptions Options { get; init; }
        };

        private static List<CombinatorTestData<string>> _combinatorTestCases = new()
        {
            new CombinatorTestData<string>
            {
                Elements = new List<string>
                {
                    "a", "b", "c"
                },
                Options = new CombinatorOptions
                {
                    IsOrdered = false
                },
                Expected = new List<List<string>>
                {
                    new () {"a", "a", "a"},
                    new () {"b", "a", "a"},
                    new () {"c", "a", "a"},
                    new () {"b", "b", "a"},
                    new () {"c", "b", "a"},
                    new () {"c", "c", "a"},
                    new () {"b", "b", "b"},
                    new () {"c", "b", "b"},
                    new () {"c", "c", "b"},
                    new () {"c", "c", "c"},
                }
            },
            new CombinatorTestData<string>
            {
                Elements = new List<string>
                {
                    "a", "b", "c"
                },
                Options = new CombinatorOptions
                {
                    IsElementUnique = true
                },
                Expected = new List<List<string>>
                {
                    new (){"c","b", "a"},
                    new (){"b","c", "a"},
                    new (){"c","a", "b"},
                    new (){"a","c", "b"},
                    new (){"b","a", "c"},
                    new (){"a","b", "c"},
                }
            },
            new CombinatorTestData<string>
            {
                Elements = new List<string>
                {
                    "A", "b", "7"
                },
                Options = new CombinatorOptions
                {
                    Length = 2
                },
                Expected = new List<List<string>>
                {
                    new (){"A", "A"},
                    new (){"b", "A"},
                    new (){"7", "A"},
                    new (){"A", "b"},
                    new (){"b", "b"},
                    new (){"7", "b"},
                    new (){"A", "7"},
                    new (){"b", "7"},
                    new (){"7", "7"},
                }
            },
            new CombinatorTestData<string>
            {
                Elements = new List<string>
                {
                    "A", "b", "7"
                },
                Options = new CombinatorOptions
                {

                },
                Expected = new List<List<string>>
                {
                    new (){"A", "A", "A" },
                    new (){"b", "A", "A" },
                    new (){"7", "A", "A" },
                    new (){"A", "b", "A" },
                    new (){"b", "b", "A" },
                    new (){"7", "b", "A" },
                    new (){"A", "7", "A" },
                    new (){"b", "7", "A" },
                    new (){"7", "7", "A" },

                    new (){"A", "A", "b"},
                    new (){"b", "A", "b"},
                    new (){"7", "A", "b"},
                    new (){"A", "b", "b"},
                    new (){"b", "b", "b"},
                    new (){"7", "b", "b"},
                    new (){"A", "7", "b"},
                    new (){"b", "7", "b"},
                    new (){"7", "7", "b"},

                    new (){"A", "A", "7"},
                    new (){"b", "A", "7"},
                    new (){"7", "A", "7"},
                    new (){"A", "b", "7"},
                    new (){"b", "b", "7"},
                    new (){"7", "b", "7"},
                    new (){"A", "7", "7"},
                    new (){"b", "7", "7"},
                    new (){"7", "7", "7"},
                }
            },
            new CombinatorTestData<string>
            {
                Elements = new List<string>
                {
                    "A", "b", "7"
                },
                Options = new CombinatorOptions
                {
                    IsFullSet = true
                },
                Expected = new List<List<string>>
                {
                    new (){"A", "A"},
                    new (){"b", "A"},
                    new (){"7", "A"},
                    new (){"A", "b"},
                    new (){"b", "b"},
                    new (){"7", "b"},
                    new (){"A", "7"},
                    new (){"b", "7"},
                    new (){"7", "7"},

                    new (){"A", "A", "A" },
                    new (){"b", "A", "A" },
                    new (){"7", "A", "A" },
                    new (){"A", "b", "A" },
                    new (){"b", "b", "A" },
                    new (){"7", "b", "A" },
                    new (){"A", "7", "A" },
                    new (){"b", "7", "A" },
                    new (){"7", "7", "A" },

                    new (){"A", "A", "b"},
                    new (){"b", "A", "b"},
                    new (){"7", "A", "b"},
                    new (){"A", "b", "b"},
                    new (){"b", "b", "b"},
                    new (){"7", "b", "b"},
                    new (){"A", "7", "b"},
                    new (){"b", "7", "b"},
                    new (){"7", "7", "b"},

                    new (){"A", "A", "7"},
                    new (){"b", "A", "7"},
                    new (){"7", "A", "7"},
                    new (){"A", "b", "7"},
                    new (){"b", "b", "7"},
                    new (){"7", "b", "7"},
                    new (){"A", "7", "7"},
                    new (){"b", "7", "7"},
                    new (){"7", "7", "7"},
                }
            },
        };
    }
}