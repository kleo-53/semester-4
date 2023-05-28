module PrimeSequence.Tests

open NUnit.Framework
open PrimeNumbersSequence
open FsUnit

let PrimeTestCaseData () =
    [ 
        4, false
        2, true
        3, true
        5, true
        6, false
    ] |> List.map (fun (number, result) -> TestCaseData(number, result))

[<TestCaseSource(nameof PrimeTestCaseData)>]
let PrimeNumbersTest testList correctResult =
    isPrime testList |> should equal correctResult

[<Test>]
let PrimeSequenceTest () =
    let resultSequence = primeSequence() |> Seq.take 10 |> List.ofSeq
    let correctSequence = [2; 3; 5; 7; 11; 13; 17; 19; 23; 29]
    Assert.AreEqual(resultSequence, correctSequence)