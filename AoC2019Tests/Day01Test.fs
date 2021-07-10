module Day01Test

open AoC2019

open NUnit.Framework

[<Test>]
let Part1 () =
    let result = Day01.SolvePart1
    Assert.AreEqual(result, "part 1")


[<Test>]
let Part2 () = 
    let result = Day01.SolvePart2
    Assert.AreEqual(result, "part 2")

        
[<TestCase(12, 2)>]
[<TestCase(14, 2)>]
[<TestCase(1969, 654)>]
[<TestCase(100756, 33583)>]
let GetFuelTest(input : float32,  expected : float32) =
    let actual = Day01.GetFuel(input)
    Assert.AreEqual(expected, actual)