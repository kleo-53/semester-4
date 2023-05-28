namespace task_3
open System

type ConcurrentStack<'t>() =
    let mutable list = []
    let lockObject = new Object()
    member _.Push value =
        lock lockObject (fun () -> (list <- value::list))

    member _.TryPop() =
        lock lockObject (fun () -> 
            match list with
            | head :: tail -> 
                (list <- tail)
                Some head
            | [] -> None
        )