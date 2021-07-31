namespace AoC2017

open Common
open System.Collections.Generic
open System.Linq

type Day01 = 
    inherit Solution

    new (file:string) = { inherit Solution(file) }
    new (input : List<string>) = { inherit Solution(input) } 

    override this.SolvePart1() = 
        let input = this.Input.First()
        let length = input.Length - 1
        let mutable sum = 0.0
        for i in 0..length do
            let next = if i + 1 <= length then input.[i + 1] else input.[0]
            if input.[i] = next then sum <-  sum + System.Char.GetNumericValue input.[i]
        
        sum.ToString()

    override this.SolvePart2() = ""
 


