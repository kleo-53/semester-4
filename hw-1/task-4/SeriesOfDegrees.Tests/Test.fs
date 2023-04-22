module SeriesOfDegrees.Tests

open NUnit.Framework
open DegreesProgram

[<TestFixture>]
type TestClass () =
    [<TestCaseAttribute(-1, -2, "[]")>]
    [<TestCaseAttribute(-1, 6, "[]")>]
    [<TestCaseAttribute(3, -5, "[]")>]
    [<TestCaseAttribute(2, 2, "[4; 8; 16]")>]
    [<TestCaseAttribute(0, 0, "[1]")>]
    [<TestCaseAttribute(6, 0, "[64]")>]
    [<TestCaseAttribute(0, 2, "[1; 2; 4]")>]
    member _.CorrectDegreesTest(n: int, m: int, correctLine: string) =
        let result = degreeSeries n m
        let strResult = result.ToString()
        Assert.AreEqual(correctLine, strResult)