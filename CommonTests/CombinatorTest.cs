using Common;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CommonTests
{
    public class CombinatorTest
    {
        //[Test]
        public void Test( )
        {
            /*
             so basically the use cases left to cover is; 
             - make unique. Put another way; ignore combination order.
               that is; for the given set return aa, ab, bb, i.e. exclude 'ba' since that is the same as 'ab'
            
             - ignore duplicate elements. Put another way; each element can only occur once in a combination
               that is; for the given set return ab, ba, i.e. exlude aa and bb
               nore: this excludes combinations which are longer than the given element set. 

             - combine both use cases. Put another way; each element used once and ignore combination order
               that is; for the given set return ab

               implementation; 
               - api on static combinator. Pro; explicit what is does. Cons, more methods                      
               - more overloads. Pro; api stays the same. Cons, more parameter flags etc
               - extension methods on List<List<T>>, maybe even wrapped in CombinatorResult class
                  Pro; I like method chaining, keeps combinator api clean,  
                  cons; maybe more expensive in terms of filtering after the fact. i.e. rather generate proper result in one go. 
               - create options class and pass it as parameter along elements
            */
            var p = Combinator.Generate(new List<string> { "a", "b", "c" }, true);

            //every element once per combination
            var r = p.Where(r => r.GroupBy(e => e).All(g => g.Count( ) == 1));

            //ignore order, i.e. ab is equivalent to ba
            var r1 = p.Select(l => (l: l, h: l.Sum(e => ( long ) e.GetHashCode( )))).GroupBy(t => t.h).Select(g => g.First( ).l);
        }



        [TestCaseSource(nameof(CombinationTestCases))]
        public void CombinationTest(CombinationTestData<string> data)
        {
            var actual = Combinator.Generate(data.Elements, data.Options);
            Assert.AreEqual(data.Expected, actual.Result);
        }

        //[TestCaseSource(nameof(CombinationTestDataString))]
        //public void CombinatorStringTest(CombinationTestData<string> data)
        //{
        //    var actual = Combinator.Generate(data.Elements, data.isFullSet);

        //    Assert.AreEqual(data.Expected, actual);
        //}

        //[TestCaseSource(nameof(CombinationTestDataIntegers))]
        //public void CombinatorIntegerTest(CombinationTestData<int> data)
        //{
        //    var actual = Combinator.Generate(data.Elements, data.Clength);

        //    Assert.AreEqual(data.Expected, actual);
        //}

        public record CombinationTestData<T>
        {
            public T Clength { get; init; }
            public List<T> Elements { get; init; }
            public List<List<T>> Expected { get; init; }
            public CombinatorOptions Options { get; init; }
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

        private static List<CombinationTestData<string>> CombinationTestCases = new List<CombinationTestData<string>>
        {
             new CombinationTestData<string>
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
            new CombinationTestData<string>
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
            new CombinationTestData<string>
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
            new CombinationTestData<string>
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
            new CombinationTestData<string>
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