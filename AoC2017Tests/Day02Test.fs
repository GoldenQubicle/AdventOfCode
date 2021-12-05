module Day02Test

open AoC2017
open NUnit.Framework
open Common.Extensions

[<TestCase("day02test1", "18")>]
[<TestCase("day02", "54426")>]
let Part1 (file: string, expected : string) =
    let day = new Day02(file)
    let actual = day.SolvePart1()
    Assert.AreEqual(expected, actual)



[<TestCase("day02test2", "9")>]
let Part2 (file: string, expected : string) =
    let day = new Day02(file)
    let actual = day.SolvePart2()
    Assert.AreEqual(expected, actual)

