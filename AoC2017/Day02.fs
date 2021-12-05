namespace AoC2017

open Common
open System.Collections.Generic
open System.Linq 

type Day02 = 
    inherit Solution

    new (file:string) = { inherit Solution(file, "\n") }
    new (input : List<string>) = { inherit Solution(input) }

    override this.SolvePart1() = 
        this.Input
            .Select(fun line -> line.Split("\t").ToList().Select(fun e -> e |> int32))
            .Aggregate(0, fun agg row -> agg + (row.Max() - row.Min())).ToString()
           
    override this.SolvePart2() = 
        this.Input
            .Select(fun line -> line.Split("\t").ToList().Select(fun e -> e |> int32).ToArray())
            .Aggregate(0, fun agg row -> agg + this.rowSum(row)).ToString()

    member this.rowSum(row: int32[] ): int32 = 
        let mutable sum = 0
        for i in 0..row.Length-2 do 
            for j in i+1..row.Length-1 do 
                let iSmallest = row.[i] <= row.[j]
                let m = if iSmallest then row.[j] % row.[i] else row.[i] % row.[j]              
                sum <- if m = 0 && iSmallest then (row.[j] / row.[i] ) elif m = 0 then (row.[i] / row.[j] ) else sum
        sum
            
