module ReverseList.Tests

open NUnit.Framework
open ReverseListFunction

[<TestFixture>]
type TestClass () =
    [<TestCaseAttribute("1, 234, 453, 1, 4, 2, -4", "-4, 2, 4, 1, 453, 234, 1")>]
    [<TestCaseAttribute("1, 2, 3, 4, 5", "5, 4, 3, 2, 1")>]
    [<TestCaseAttribute("0, 0, 1, -1", "-1, 1, 0, 0")>]
    [<TestCaseAttribute("6", "6")>]
    [<TestCaseAttribute("-1, 6, -34", "-34, 6, -1")>]
    member _.CorrectReversingTest(line: string, correctLine: string) =
        let splitLine = line.Split(", ")
        let inputList = List.init splitLine.Length (fun i -> splitLine[i] |> int)

        let splitcorrectLine = correctLine.Split(", ")
        let intCorrectList = List.init splitcorrectLine.Length (fun i -> splitcorrectLine[i] |> int)
        Assert.AreEqual(intCorrectList, (reverse inputList))