module task_3.Tests

open NUnit.Framework
open task_3
open FsUnit

[<Test>]
let TestPopFromEmptyStack () =
    let testStack = new ConcurrentStack<int>()
    testStack.TryPop() |> should equal None

[<Test>]
let TestStack () =
    let testStack = new ConcurrentStack<int>()
    testStack.Push 1
    testStack.Push 10
    testStack.Push 20
    testStack.TryPop() |> should equal (Some 20)
    testStack.TryPop() |> should equal (Some 10)
    testStack.TryPop() |> should equal (Some 1)
    testStack.TryPop() |> should equal None