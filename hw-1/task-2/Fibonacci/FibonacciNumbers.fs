// Multiplying two matrix 2x2
let multiply [[a1, a2], [a3, a4]] [[b1, b2], [b3, b4]] =
    [[a1 * b1 + a2 * b3, a1 * b2 + a2 * b4], [a3 * b1 + a4 * b3, a3 * b2 + a4 * b4]]

// First Fibonacci number
let matrix () = 
    [[0, 1], [1, 1]]

// Unit matrix
let eMatrix () = 
    [[1, 0], [0, 1]]

// Fast exponentiation of the matrix
let rec binPow result matrix n = 
    match n with
    | 0 -> result
    | n when n % 2 = 1 ->
        let result = multiply result matrix
        let matrix = multiply matrix matrix
        binPow result matrix ((n - 1) / 2)
    | n when n % 2 = 0 ->
        let matrix = multiply matrix matrix
        binPow result matrix (n / 2)

// Calculates Fibonacci number by given number n
let fib n =
    match n with 
    | n when n < 0 -> -1
    | 0 -> 0
    | n ->
        let result = binPow (eMatrix ()) (matrix ()) n
        match result with
        | [[a1, a2], [b1, b2]] -> b1

let n = System.Console.ReadLine() |> int
let result = fib n
match result with 
    | -1 -> printfn "Such Fibonacci number does not exist"
    | 0 -> printfn "The Fibonacci number 0 is 0"
    | res -> printfn $"The Fibonacci number {n} is {res}"
   