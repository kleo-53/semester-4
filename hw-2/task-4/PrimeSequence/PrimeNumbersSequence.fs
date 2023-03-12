module PrimeNumbersSequence

// Checks if number is prime
let isPrime (n : int) : bool =
    let s = int (System.Math.Round(sqrt (n : float))) + 1
    let sqrtN = match s with
    | s when s >= n -> n - 1
    | s -> s

    let rec divisibility sqrtN =
        List.map (fun x -> if n % x = 0 then 1 else 0 ) [2 .. sqrtN] |> List.sum
    printf $"{(divisibility sqrtN)}, {sqrtN},,, {[2 .. sqrtN]}"
    (divisibility sqrtN) = 0

// Generate an infinite prime sequence
let primeSequence number =
    let rec sequence (n : int) =
        seq {
            if (isPrime n) then
                yield n
            yield! sequence (n + 1)
        }
    sequence 2
