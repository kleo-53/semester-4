// Counting factorial by number
let factorial number = 
    if number < 0 then -1
    else
        let rec factorialRecursive number result =
            match number with
            | 0 | 1 -> result
            | number -> factorialRecursive (number - 1) (number * result)
        factorialRecursive number 1

printf "Enter number to count factorial: "
let number = System.Console.ReadLine() |> int
let result = factorial number
match result with
    | -1 -> printf "Can not count factorial by negative number"
    | result -> printf $"Factorial of {number} is {result}"