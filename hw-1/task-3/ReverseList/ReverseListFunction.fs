// Reverse list function
let reverse list = 
    let rec reverseList givenList resultList =
        match givenList with
        | [] -> resultList 
        | head :: tail ->
            let resultList = head :: resultList
            reverseList tail resultList
    reverseList list []
    
printf "Enter elements of the list in one line: "
let inputLine = System.Console.ReadLine()
if inputLine = "" then printf "Can not reverse list without elements"
else
    let inputLine = inputLine.Split(" ")
    let inputList = List.init inputLine.Length (fun i -> inputLine[i] |> int)
    let result = reverse inputList
    printf "Reversed list is: "
    result |> Seq.iter (printf "%d ")
