// Reverse list function
let reverse list = 
    let rec reverseList givenList resultList =
        match givenList with
        | [] -> resultList 
        | head :: tail ->
            let resultList = head :: resultList
            reverseList tail resultList
    reverseList list []
    
// Creates list with numbers [2^n; .. 2^(n + m)] by given n, m
let degreeSeries n m = 
    match (n, m) with 
    | (n, m) when n < 0 || m < 0 -> []
    | (n, m) ->
        let rec degrees list k = 
            match k with
            | 0 -> list
            | k -> 
                match list with
                | head::tail ->
                    let newHead = head * 2.0
                    degrees (newHead :: list) (k - 1)
                | [] -> []
        let descResult = degrees [2.0 ** n] m
        reverse descResult

printf "Enter n: "
let n = System.Console.ReadLine() |> int
printf "Enter m: "
let m = System.Console.ReadLine() |> int
let result = degreeSeries n m
match result with
    | [] -> printf "Incorrect numbers"
    | result ->
        printf "Reversed list is: "
        result |> Seq.iter (printf "%A ")