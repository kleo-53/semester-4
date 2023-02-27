module FirstIndexOfNumber.Tests

open NUnit.Framework
open FindingIndexProgram

[<TestFixture>]
type TestClass () =
    [<TestCaseAttribute("1 2 3 4 5", 3, 2)>]
    [<TestCaseAttribute("1 1 2 2 1", 1, 0)>]
    [<TestCaseAttribute("0 -4 2", 1, -1)>]
    [<TestCaseAttribute("2", 2, 0)>]
    [<TestCaseAttribute("1", -1, -1)>]
    member _.CorrectReversingTest(line: string, element: int, correctIndex: int) =
        let splitLine = line.Split(" ")
        let inputList = List.init splitLine.Length (fun i -> splitLine[i] |> int)
        Assert.AreEqual(correctIndex, (indexOf element inputList))