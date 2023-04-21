module task_2.Tests

open NUnit.Framework

[<Test>]
let Test1 () =
    let testRomb = "   *" + "\n" + "  ***" + "\n" + " *****" 
    + "\n" + "*******" + "\n" + " *****" + "\n" + "  ***" + "\n" + "   *"
    (printRomb 4) should equal testRomb
    (printRomb 0) should equal ""
    (printRomb 1) should equal "*"