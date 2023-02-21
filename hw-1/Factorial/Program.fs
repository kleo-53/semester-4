let rec factorial x =
    if x = 1 then
        1
    else
        x * factorial (x - 1)

let n = System.Console.ReadLine() |> int

printfn "%A" (factorial n)