module Lazy.Tests

open NUnit.Framework
open FsUnit
open System.Threading
open System.Threading.Tasks

let lazies supplier : ILazy<'a> list =
    [ SimpleLazy<'a>(supplier)
      ThreadsLazy<'a>(supplier)
      LockFreeLazy<'a>(supplier) ]

[<Test>]
let CorrectWorkInOneThread () =
    let supplier = fun x -> 0

    lazies supplier
    |> List.iter (fun (currentLazy:ILazy<int>) ->
        let result = currentLazy.Get()
        result |> should equal 0
        currentLazy.Get() |> should equal result
        currentLazy.Get() |> should equal result)

[<Test>]
let MultiThreadsGetCalculatesOnce () =
    let mutable counter = 0
    let supplier = fun () -> counter <- Interlocked.Increment(ref counter)
    let myLazy = ThreadsLazy<unit>(supplier) :> ILazy<unit>
    Parallel.For(0, 10, (fun obj -> myLazy.Get())) |> ignore
    counter |> should equal 1

[<Test>]
let lazyLockFreeTest () =
    let mutable counter = 0
    let manualResetEvent = new ManualResetEvent false
    let supplier = 
        fun () -> 
            manualResetEvent.WaitOne() |> ignore
            counter <- Interlocked.Increment(ref counter)
    let lazyLockFree = LockFreeLazy<unit>(supplier) : ILazy<unit>
    manualResetEvent.Set() |> ignore
    let tasks = Seq.init 10 (fun _ ->
        async { return (lazyLockFree.Get()) })
    tasks
    |> Async.Parallel
    |> Async.RunSynchronously |> ignore
    counter |> should equal 1