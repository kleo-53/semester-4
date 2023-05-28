module task_1.Tests

open NUnit.Framework
open task_1
open RoundingWorkflow
open FsUnit
open System

[<Test>]
let ``Division test`` () =
    let res = Builder 3 {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    }
    res |> should equal 0.048

[<Test>]
let ``General test`` () =
    let res = Builder 6 {
        let! a = 2.0 / 12.0
        let! b = 3.5
        let! c = 0.1 + 0.4
        return a * b - c
    }
    res |> should equal 0.083335

[<Test>]
let ``Division by zero test`` () =
    let res = Builder 6 {
        let! a = 32.0
        let! b = 0
        return a / b
    }
    res |> should equal (Double.PositiveInfinity)