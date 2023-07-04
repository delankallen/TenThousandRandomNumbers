open ShuffleArray

let MAX = 10_000
let arrayToShuffle = [| 1..MAX |]

shuffleArray arrayToShuffle |> List.iter (fun x -> printfn $"{x}")
