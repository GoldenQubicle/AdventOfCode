module AoC2019Tests

open NUnit.Framework

[<SetUp>]
let Setup () =
    ()

[<Test>]
let Test1 () =
    AoC2019.Say.hello("world")
