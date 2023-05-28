module task_2.Tests

open NUnit.Framework
open task_2
open FsUnit

[<Test>]
let Test1 () =
    let testRomb = "   *" + "\n" + "  ***" + "\n" + " *****" + "\n" + "*******" + "\n" + " *****" + "\n" + "  ***" + "\n" + "   *"
    (Romb.printRomb 4) |> should equal testRomb
    (Romb.printRomb 0) |> should equal ""
    (Romb.printRomb 1) |> should equal "*"