module Fibonacci.Tests

open NUnit.Framework
open FibonacciNumbers

[<TestFixture>]
type TestClass () =
    [<TestCaseAttribute(-1634, -1)>]
    [<TestCaseAttribute(-1, -1)>]
    [<TestCaseAttribute(0, 0)>]
    [<TestCaseAttribute(1, 1)>]
    [<TestCaseAttribute(2, 1)>]
    [<TestCaseAttribute(3, 2)>]
    [<TestCaseAttribute(4, 3)>]
    [<TestCaseAttribute(5, 5)>]
    [<TestCaseAttribute(6, 8)>]
    [<TestCaseAttribute(22, 17711)>]
    [<TestCaseAttribute(23, 28657)>]
    member _.CorrectCalculationTest(value: int, correctResult: int) =
        Assert.AreEqual(correctResult, (fib value))
