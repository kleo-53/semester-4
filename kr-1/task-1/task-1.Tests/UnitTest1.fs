module task_1.Tests

open NUnit.Framework

[<Test>]
let Test1 () =
    let testList = [sin 1.0; cos 1.0; sin 2.0; cos 2.0; sin 3.0; cos 3.0]
    supermap [1.0; 2.0; 3.0] (fun x -> [sin x; cos x]) should equal testList
