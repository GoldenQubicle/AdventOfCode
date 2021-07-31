module Day01Test

open AoC2017
open NUnit.Framework

[<TestCase("1122","3")>] 
[<TestCase("1111","4")>] 
[<TestCase("1234","0")>] 
[<TestCase("91212129","9")>]
let Part1 (input: string, expected : string) =
    let actual = Day01.SolvePart1
    Assert.AreEqual(expected, actual)

[<Test>]
let Part2 () = 
    let actual = Day01.SolvePart2
    Assert.AreEqual("", actual)