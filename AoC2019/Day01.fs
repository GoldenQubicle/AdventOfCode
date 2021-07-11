namespace AoC2019

open System
open System.IO

module Day01 = 

    let input file = File.ReadLines(AppContext.BaseDirectory + file) |> Seq.map(fun line -> line |> int )

    let GetFuel (mass : int) : int = Math.Floor((float mass) / 3.0 ) - 2.0 |> int

    let GetFuelRecursive(mass : int) : int = 
        let rec loop mass acc =
            match mass with 
            | var1 when var1 > 6 -> 
                let newMass = GetFuel mass
                loop newMass (acc + newMass)
            | _ -> acc

        loop mass 0


    let SolvePart1 : string = input("data/day01.txt") |> Seq.map(fun i -> GetFuel(i) ) |> Seq.sum |> string

    let SolvePart2 : string = input("data/day01.txt") |> Seq.map(fun i -> GetFuelRecursive(i) ) |> Seq.sum |> string



