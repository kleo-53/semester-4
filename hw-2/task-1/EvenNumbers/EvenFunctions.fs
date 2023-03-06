let evenMap list = 
    List.map (fun x -> if x % 2 = 0 then 1 else 0 ) list |> List.sum

let evenFilter list = 
    List.filter (fun x -> x % 2 = 0) list |> List.length

let evenFold list = 
    List.fold (fun acc x -> if x % 2 = 0 then acc + 1 else acc) 0 list
    
let inputLine = System.Console.ReadLine()
let inputLinee = inputLine.Split(" ")
let inputList = List.init inputLinee.Length (fun i -> inputLinee[i] |> int)
let result = evenFold inputList
printfn $"{result}"