namespace Lazy

open System.Threading

type LockFreeLazy<'a> (supplier : unit -> 'a) =
    let mutable result = None

    interface ILazy<'a> with
        member this.Get() =
            if result.IsNone then 
                let calc = Some (supplier())
                Interlocked.CompareExchange(&result, calc, None) |> ignore
            result.Value