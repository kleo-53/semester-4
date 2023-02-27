// Counting factorial by number
let rec factorial number =
    match number with
    | n when n < 0 -> -1
    | 1 | 0 -> 1
    | number -> number * factorial (number - 1)

printf "Enter number to count factorial: "
let number = System.Console.ReadLine() |> int
let result = factorial number
match result with
    | -1 -> printf "Can not count factorial by negative number"
    | result -> printf $"Factorial of {number} is {result}"