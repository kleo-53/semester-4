open FsCheck

let func x l = List.map (fun y -> y * x) l

let func1 x : (int) list -> (int) list =
    List.map (fun y -> x * y)

let func2 x : (int) list -> (int) list =
    List.map ((*) x)
    
let func3 : int -> (int) list -> (int) list =
    List.map << (*)

let checking (x:int) (l:list<int>) = (func x l) = (func3 x l) 
Check.QuickThrowOnFailure checking 