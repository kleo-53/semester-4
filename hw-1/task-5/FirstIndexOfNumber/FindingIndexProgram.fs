// Finds first index of given element in given list
let indexOf element list = 
    let rec find element list index = 
        match list with
        | [] -> -1
        | head :: tail -> 
            match head with
            | head when head = element -> index
            | _ ->
                find element tail (index + 1)
    find element list 0


printf "Enter elements of list in one line: "
let inputLine = System.Console.ReadLine()
printf "Enter a number to search for: "
let element = System.Console.ReadLine() |> int
match inputLine with
| "" -> printf "Can not reverse list without elements"
| inputLine ->
    let inputLine = inputLine.Split(" ")
    let inputList = List.init inputLine.Length (fun i -> inputLine[i] |> int)
    let result = indexOf element inputList
    match result with
    | -1 -> printf "Not such element in list"
    | result -> printf $"First index of the element {element} is {result}"