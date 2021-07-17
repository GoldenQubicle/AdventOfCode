namespace AoC2019

open System
open System.IO

module Day01 = 

    let basePath : string = AppContext.BaseDirectory.Replace("CLI", "AoC2019") // check to make sure when getting called from cli .. could probably be nicer

    let input file = File.ReadLines(basePath + file) |> Seq.map(fun line -> line |> int )

    let GetFuel (mass : int) : int = Math.Floor((float mass) / 3.0 ) - 2.0 |> int

    let GetFuelRecursive(mass : int) : int = 
        let rec loop mass total =
            if mass > 6 then
                let fuelMass = GetFuel mass
                loop fuelMass total + fuelMass
            else total
        loop mass 0


    let SolvePart1 : string = input("data/day01.txt") |> Seq.map(fun i -> GetFuel(i) ) |> Seq.sum |> string

    let SolvePart2 : string = input("data/day01.txt") |> Seq.map(fun i -> GetFuelRecursive(i) ) |> Seq.sum |> string



