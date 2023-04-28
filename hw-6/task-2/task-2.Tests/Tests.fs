module task_2.Tests

open NUnit.Framework
open FsUnit
open StringWorkflow

[<Test>]
let CorrectStringCalculationTest () =
    let result = calculate () {
        let! x = "1"
        let! y = "2"
        let z = x + y
        return z
    }
    result |> should equal (Some 3)

[<Test>]
let IncorrectCalculationTest () =
    let result = calculate () {
        let! x = "1"
        let! y = "Ú"
        let z = x + y
        return z
    }
    result |> should equal None