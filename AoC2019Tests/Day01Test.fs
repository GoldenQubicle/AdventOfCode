module Day01Test

open AoC2019

open NUnit.Framework

[<Test>]
let Part1 () =
    let result = Day01.SolvePart1
    Assert.AreEqual(result, "3331849")


[<Test>]
let Part2 () = 
    let result = Day01.SolvePart2
    Assert.AreEqual(result, "4994898")

        
[<TestCase(12, 2)>]
[<TestCase(14, 2)>]
[<TestCase(1969, 654)>]
[<TestCase(100756, 33583)>]
let GetFuelTest(input : int,  expected : int) =
    let actual = Day01.GetFuel(input)
    Assert.AreEqual(expected, actual)


[<TestCase(14, 2)>]
[<TestCase(1969, 966)>]
[<TestCase(100756, 50346)>]
let GetFuelRecursiveTest(input: int, expected: int) = 
    let actual = Day01.GetFuelRecursive(input)
    Assert.AreEqual(expected, actual)
