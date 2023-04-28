namespace Lazy

open System.Threading

type LockFreeLazy<'a> (supplier : unit -> 'a) =
    let mutable result = None
    let mutable currentVal = None
    let mutable startVal = result
    let mutable isDone = false

    interface ILazy<'a> with
        member this.Get() =
            if result.IsNone then 
                let current = result
                let calc = Some (supplier())
                while not <| obj.ReferenceEquals(result, Interlocked.CompareExchange(&result, calc, current)) do
                    let current = result
                    let calc = Some (supplier())
            result.Value