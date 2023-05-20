module EvenNumbers.Tests

open FsCheck
open EvenFunctions
open NUnit.Framework
open FsUnit

let EqualFunctionsTest (testList:list<int>) = (evenMap testList = evenFilter testList && evenFilter testList = evenFold testList)

Check.QuickThrowOnFailure EqualFunctionsTest

let EvenFunctionsTestCaseData () =
    [ 
        [], 0
        [ 1; 2; 3 ], 1
        [ -2; 2; 0 ], 3
        [ 1; 3; 5 ; 7; 9; -1], 0 
        [1], 0
    ] |> List.map (fun (testList, result) -> TestCaseData(testList, result))

[<TestCaseSource(nameof EvenFunctionsTestCaseData)>]
let evenNumbersTest testList correctResult =
    evenMap testList |> should equal correctResult
