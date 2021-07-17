module Day02Test

open AoC2019
open NUnit.Framework

[<TestCase("1,0,0,0,99","2,0,0,0,99")>] 
[<TestCase("2,3,0,3,99","2,3,0,6,99")>]
let Part1 (input: string, expected : string) =
    Day02.input <- fun _ -> input.Split(',') :> seq<string>
    let actual = Day02.SolvePart1
    Assert.AreEqual(expected, actual)

[<Test>]
let Part2 () = 
    let actual = Day02.SolvePart2
    Assert.AreEqual("", actual)