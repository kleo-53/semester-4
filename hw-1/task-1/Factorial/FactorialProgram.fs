// Counting factorial by number
let rec factorial number =
    if number < 0 then -1
    else if number = 1 || number = 0 then 1
    else
        number * factorial (number - 1)

printf "Enter number to count factorial: "
let number = System.Console.ReadLine() |> int
let result = factorial number
if result = -1 then printf "Can not count factorial by negative number"
else printf $"Factorial of {number} is {result}"