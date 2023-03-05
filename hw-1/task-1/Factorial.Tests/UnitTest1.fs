module Factorial.Tests

open NUnit.Framework
open FactorialProgram

[<TestFixture>]
type TestClass () =
    [<TestCaseAttribute(-123, -1)>]
    [<TestCaseAttribute(-1, -1)>]
    [<TestCaseAttribute(0, 1)>]
    [<TestCaseAttribute(1, 1)>]
    [<TestCaseAttribute(2, 2)>]
    [<TestCaseAttribute(3, 6)>]
    [<TestCaseAttribute(5, 120)>]
    member _.CountingFactorialTest(value: int, correctResult: int) =
        Assert.AreEqual(correctResult, (factorial value))