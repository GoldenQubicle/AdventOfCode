using Common;
using NUnit.Framework;
using System.Collections.Generic;

namespace CommonTests
{
    public class CombinatorTest
    {       
        [TestCaseSource(nameof(CombinatorTestCases))]
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

        private static List<CombinatorTestData<string>> CombinatorTestCases = new List<CombinatorTestData<string>>
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
                    new List<string>{"a", "a", "a"},
                    new List<string>{"b", "a", "a"},
                    new List<string>{"c", "a", "a"},
                    new List<string>{"b", "b", "a"},
                    new List<string>{"c", "b", "a"},
                    new List<string>{"c", "c", "a"},
                    new List<string>{"b", "b", "b"},
                    new List<string>{"c", "b", "b"},
                    new List<string>{"c", "c", "b"},
                    new List<string>{"c", "c", "c"},
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
                    new List<string>{"c","b", "a"},
                    new List<string>{"b","c", "a"},
                    new List<string>{"c","a", "b"},
                    new List<string>{"a","c", "b"},
                    new List<string>{"b","a", "c"},
                    new List<string>{"a","b", "c"},
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
        };     
    }
}