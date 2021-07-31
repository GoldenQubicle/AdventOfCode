module Day01Test

open AoC2017
open NUnit.Framework
open System.Collections.Generic
open Common.Extensions

[<TestCase("1122","3")>] 
[<TestCase("1111","4")>] 
[<TestCase("1234","0")>] 
[<TestCase("91212129","9")>]
let Part1 (input: string, expected : string) =
    let list = new List<string>();
    let day = new Day01(list.Expand(input))
    let actual = day.SolvePart1()
    Assert.AreEqual(expected, actual)

[<TestCase("1212","6")>] 
[<TestCase("1221","0")>] 
[<TestCase("123425","4")>] 
[<TestCase("123123","12")>]
[<TestCase("12131415","4")>]
let Part2 (input: string, expected : string) = 
    let list = new List<string>();    
    let day = new Day01(list.Expand(input))
    let actual = day.SolvePart2()
    Assert.AreEqual(expected, actual)

[<Test>]
let Part1Solution() = 
    let day = new Day01("day01")
    let actual = day.SolvePart1()
    Assert.AreEqual("1182", actual)

[<Test>]
let Part2Solution() = 
    let day = new Day01("day01")
    let actual = day.SolvePart2()
    Assert.AreEqual("1152", actual)