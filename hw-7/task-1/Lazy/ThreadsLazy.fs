namespace Lazy

open System

type ThreadsLazy<'a> (supplier : unit -> 'a) =
    [<VolatileField>]
    let mutable result = None
    let lockObj = new Object()

    interface ILazy<'a> with
        member this.Get() =
            if result.IsNone then 
                lock lockObj (fun () -> result <-Some (supplier()))
            result.Value