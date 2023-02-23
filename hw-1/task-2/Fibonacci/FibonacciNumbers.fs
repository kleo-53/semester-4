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
    if n = 0 then result
    else if n % 2 = 1 then
        let result = multiply result matrix
        let matrix = multiply matrix matrix
        binPow result matrix ((n - 1) / 2)
    else
        let matrix = multiply matrix matrix
        binPow result matrix (n / 2)

// Calculates Fibonacci number by given number n
let fib n =
    if n < 0 then -1
    else if n = 0 then 0
    else
        let result = binPow (eMatrix ()) (matrix ()) n
        match result with
        | [[a1, a2], [b1, b2]] -> b1

let n = System.Console.ReadLine() |> int
let result = fib n
if result = -1 then printfn "Such Fibonacci number does not exist"
else if result = 0 then printfn "The Fibonacci number 0 is 0"
else printfn $"The Fibonacci number {n} is {fib n}"
   