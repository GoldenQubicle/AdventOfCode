module Day03Test

open AoC2017
open NUnit.Framework
open Common.Extensions

[<TestCase("1","0")>] 
[<TestCase("12","3")>] 
[<TestCase("23","2")>] 
[<TestCase("1024","31")>]
let Part1 (input: string, expected : string) =
    let day = new Day03(input.ToList())
    let actual = day.SolvePart1()
    Assert.AreEqual(expected, actual)

[<Test>]
let Part2 () = 
    let day = new Day03("day03")
    let actual = day.SolvePart2()
    Assert.AreEqual("", actual)