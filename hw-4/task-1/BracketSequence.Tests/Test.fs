module BracketSequence.Tests

open NUnit.Framework
open BracketsCheck

[<TestFixture>]
type TestClass () =
    [<TestCaseAttribute("()[]{}", true)>]
    [<TestCaseAttribute(")(", false)>]
    [<TestCaseAttribute("({)}", false)>]
    [<TestCaseAttribute("([{()}])", true)>]
    [<TestCaseAttribute("((()))", true)>]
    [<TestCaseAttribute("[[]([)]]", false)>]
    [<TestCaseAttribute("", true)>]
    [<TestCaseAttribute("(1 + 3) * (4 - 2) - 1", true)>]
    member _.BracketsCheckTest(brackets: string, correctResult: bool) =
        let result = Check brackets
        Assert.AreEqual(correctResult, result)