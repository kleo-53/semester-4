namespace Lazy

open System

type ThreadsLazy<'a> (supplier : unit -> 'a) =
    [<VolatileField>]
    let mutable result: 'a Option = None
    let lockObj = new Object()

    interface ILazy<'a> with
        member this.Get() =
            if result.IsNone then 
                lock lockObj (fun () ->
                    if result.IsNone then
                        result <- Some (supplier())
                    result.Value)
            else
                result.Value