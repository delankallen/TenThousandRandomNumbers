# TenThousandRandomNumbers
This program will generate a list of 10,000 unique random numbers between 1-10,000 (inclusive).
The output is printed to the console.

## Workflow
My workflow for this challenge:

1. Read challenge text.
2. Wrote out requirements.
    * Generate a list of 10,000 numbers in random order each time.
    * Each number must be between 1-10,000 (inclusive).
    * Each number must be unique.
3. The simplest solution is creating an array of range 1-10,000 and shuffling the array order.
    * Shuffling a range of numbers satisfied uniqueness and limit requirements.
4. Found Fisher-Yates shuffle by searching for array shuffling algorithm.
5. Implement basic version of algorithm in script with F# interactive.
6. Implement algorithm in .Net F# console application with unit tests covering requirements.
7. Write documentation

## Build
```
git clone https://github.com/delankallen/TenThousandRandomNumbers.git
cd ./TenThousandRandomNumbers
dotnet build
```

## Run

This will output the list to a file called `TenThousandRandomNumbers.txt`

```
dotnet run --project ./TenThousandRandomNumbers/TenThousandRandomNumbers.fsproj > TenThousandRandomNumbers.txt
```

## Tests
Tests are setup using [xUnit](https://xunit.net/). 

To run tests:

```
dotnet test
```

There are four tests:
 * List length is 10,000
    ```F#
    [<Fact>]
    let ``List length is 10,000`` () =
        let wat = shuffleArray [|1..MAX|]
        Assert.True(wat.Length = MAX)
    ```
 * Each number is unique
    ```F#
    [<Fact>]
    let ``Each Number In List Is Unique`` () =
        let arr = [|1..MAX|] |> shuffleArray |> Set
        Assert.True(arr.Count = MAX)
    ```
 * Maximum number is 10,000
    ```F#
    [<Fact>]
    let ``Maximum number is 10,000`` () =
        let maxNum = [|1..MAX|] |> shuffleArray |> Seq.max
        Assert.True((maxNum = MAX))
    ```
 * Minimum number is 1 
    ```F#
    [<Fact>]
    let ``Minimum number is 1`` () =
        let minNum = [|1..MAX|] |> shuffleArray |> Seq.min
        Assert.True((minNum = 1))
    ```

Tests are located in [TenThousandRandomNumbers.Test/Tests.fs](./TenThousandRandomNumbers.Test/Tests.fs)

# Program

1. Create a new array of range 1 to 10,000: 
    ```F#
    let MAX = 10_000
    let arrayToShuffle = [|1..MAX|]
    ```
2. Pass array to `shuffleArray`
    ```F#
    shuffleArray arrayToShuffle
    ```
3. Print each item of the shuffled array
    ```F#
    shuffleArray arrayToShuffle
    |> List.iter (fun x -> printfn $"{x}")
    ```

Full program:

    open ShuffleArray

    let MAX = 10_000
    let arrayToShuffle = [|1..MAX|]

    shuffleArray arrayToShuffle
    |> List.iter (fun x -> printfn $"{x}")
        
# shuffleArray Function

The `shuffleArray` function takes an input array of type `'a` and shuffles its elements randomly using the [Fisher-Yates shuffle](https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle). The function returns the array with the elements rearranged in a random order. This function shuffles the elements in place, meaning it modifies the original array. The Fisher-Yates shuffle takes O(n) time as it only has to iterate the array one time.

The function is located in [TenThousandRandomNumbers/ShuffleArray.fs](TenThousandRandomNumbers/ShuffleArray.fs).

## Signature

```F#
shuffleArray : array<'a> -> 'a list
```

## Parameters

* `arr : array<'a>` : The input array to be shuffled

## Return Value

* `a list` : A new list containing the elements of the array, shuffled randomly

## Breakdown

Step by step of the `shuffleArray` function:

1. Define a nested function called `swap`, which swaps two values at the indices of x and y.
    ```F#
    let swap x y =
        let tmp = arr[x]
        arr[x] <- arr[y]
        arr[y] <- tmp
    ```
2. Create a new instance of the `Random` class.
3. Get length of input array `let ln = arr.Length`
4. Create range from 0 to input array's length minus 2. This range represents the indices of the elements in the array that will be swapped.
    ```F#
    let rnd = new Random()
    let ln = arr.Length
    [0..(ln-2)]
    ```
5. Iterate over each index `x` in the range using the `Seq.iter` function.
    * Generate a random index `y` using `rnd.Next(x, ln)`. This index will be between `x` (inclusive) and `ln`(exclusive), where `ln` is the length of the array.
    * Call the swap function to swap the elements at indices x and y.

    ```F#
    [0..(ln-2)] |> Seq.iter (fun x -> swap x (rnd.Next(x, ln))) 
    ```
6. Convert shuffled array to a list.
7. Return list

## Full function

```F#
let shuffleArray (arr: array<'a>) =
    let swap x y = 
        let tmp = arr[x]
        arr[x] <- arr[y]
        arr[y] <- tmp
    let rnd = new Random()
    let ln = arr.Length
    [0..(ln-2)]
    |> Seq.iter (fun x -> swap x (rnd.Next(x, ln))) 
    arr |> Seq.toList
``` 

## Example

```F#
let myArray = [|1; 2; 3; 4; 5|]
let shuffledArray = shuffleArray myArray
printfn "%A" shuffledArray
```

Output:
```F#
[|3; 4; 2; 1; 5|]
```

