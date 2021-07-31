module Day01Test

open AoC2017
open NUnit.Framework
open System.Collections.Generic

[<TestCase("1122","3")>] 
[<TestCase("1111","4")>] 
[<TestCase("1234","0")>] 
[<TestCase("91212129","9")>]
let Part1 (input: string, expected : string) =
    let list = new List<string>();
    list.Add(input)
    let day = new Day01(list)
    let actual = day.SolvePart1()
    Assert.AreEqual(expected, actual)

[<Test>]
let Part2 () = 
    let day = new Day01(new List<string>())
    let actual = day.SolvePart2()
    Assert.AreEqual("", actual)

[<Test>]
let Part1Solution() = 
    let day = new Day01("day01")
    let actual = day.SolvePart1()
    Assert.AreEqual("1182", actual)