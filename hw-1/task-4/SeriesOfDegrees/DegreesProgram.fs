// Reverse list function
let reverse list = 
    let rec reverseList givenList resultList =
        if givenList = [] then resultList 
        else
            let resultList = givenList.Head :: resultList
            reverseList givenList.Tail resultList
    reverseList list []
    
// Creates list with numbers [2^n; .. 2^(n + m)] by given n, m
let degreeSeries n m = 
    if (n < 0 || m < 0) then []
    else
        let rec degrees list k = 
            if k = 0 then list
            else
                let newHead = (List.head list) * 2.0
                degrees (newHead :: list) (k - 1)
        let descResult = degrees [2.0 ** n] m
        reverse descResult

printf "Enter n: "
let n = System.Console.ReadLine() |> int
printf "Enter m: "
let m = System.Console.ReadLine() |> int
let result = degreeSeries n m
if result = [] then printf "Incorrect numbers"
else 
    printf "Reversed list is: "
    result |> Seq.iter (printf "%A ")