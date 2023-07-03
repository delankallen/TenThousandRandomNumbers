open ShuffleArray

let MAX = 10_000
let lst = [|1..MAX|]

shuffleArray lst
|> List.iter (fun x -> printfn $"{x}")
