// Finds first index of given element in given list
let indexOf element list = 
    let rec find element list index = 
        if list = [] then -1
        else
            if List.head list = element then index
            else
                find element list.Tail (index + 1)
    find element list 0


printf "Enter elements of list in one line: "
let inputLine = System.Console.ReadLine()
printf "Enter a number to search for: "
let element = System.Console.ReadLine() |> int
if inputLine = "" then printf "Can not reverse list without elements"
else
    let inputLine = inputLine.Split(" ")
    let inputList = List.init inputLine.Length (fun i -> inputLine[i] |> int)
    let result = indexOf element inputList
    if result = -1 then printf "Not such element in list"
    else
        printf $"First index of the element {element} is {result}"