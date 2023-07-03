module Tests

open Xunit
open ShuffleArray

let MAX = 10_000

[<Fact>]
let ``My test`` () =
    let wat = shuffleArray [|1..MAX|]
    wat |> ignore
    Assert.True(true)

// Generates a list of 10,000 numbers in random order each time
[<Fact>]
let ``List length is 10,000`` () =
    let wat = shuffleArray [|1..MAX|]
    Assert.True(wat.Length = MAX)

// Each number in list is unique
[<Fact>]
let ``Each Number In List Is Unique`` () =
    let arr = [|1..MAX|] |> shuffleArray |> Set
    Assert.True(arr.Count = MAX)

// Each number is between 1 and 10,000 inclusive
[<Fact>]
let ``Maximum number is 10,000`` () =
    let maxNum = [|1..MAX|] |> shuffleArray |> Seq.max
    Assert.True((maxNum = MAX))

[<Fact>]
let ``Minimum number is 1`` () =
    let minNum = [|1..MAX|] |> shuffleArray |> Seq.min
    Assert.True((minNum = 1))