namespace AoC2017

open Common
open System.Collections.Generic
open System.Linq 
open System.Numerics

type Day03 = 
    inherit Solution

    new (file:string) = { inherit Solution(file) }
    new (input : List<string>) = { inherit Solution(input) }

    override this.SolvePart1() = 
        let size = ceil ( sqrt (this.Input.First() |> float )) |> int
        let isEven = size % 2 = 0 
        let offset = pown size 2 - (this.Input.First() |> int) |> fun (o:int) -> if isEven then o / 2 else o / 2 - 1
        let distance = if isEven then size - offset else size - offset - 1
        distance.ToString()

    override this.SolvePart2() = "" 