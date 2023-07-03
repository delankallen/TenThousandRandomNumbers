module ShuffleArray

open System

///<summary>Shuffles the given array using the Fisher-Yates Algorithm</summary>
///<param name="arr">Array to be shuffled</param>
let shuffleArray (arr: array<'a>) =
    let swap x y = // Swaps two vaules at the index of x and y
        let tmp = arr[x]
        arr[x] <- arr[y]
        arr[y] <- tmp
    let rnd = new Random()
    let ln = arr.Length
    [0..(ln-2)]
    |> Seq.iter (fun x -> swap x (rnd.Next(x, ln))) //Iterates throgh the list one time to swap each item at a random index
    arr |> Seq.toList