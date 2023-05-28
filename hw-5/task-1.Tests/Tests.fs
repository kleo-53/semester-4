module task_1.Tests

open NUnit.Framework
open System.Collections.Generic
open Network
open FsUnit

[<Test>]
let Test1 () =
    let osProb = Map [ ("Windows", 0.7); ("Linux", 0.4); ("MacOS", 0.5); ("Other", 0.9)]

    let computers = [Computer(0, "Windows", false, osProb);
    Computer(1, "Linux", true, osProb);
    Computer(2, "Linux", false, osProb);
    Computer(3, "MacOS", false, osProb);
    Computer(4, "Windows", false, osProb);
    Computer(5, "MacOS", false, osProb);
    Computer(6, "Linux", true, osProb);
    Computer(7, "Other", false, osProb);
    Computer(8, "Other", false, osProb)]
    
    let connection = Dictionary<int, int list>()
    connection.Add(0, [2; 5; 8])
    connection.Add(1, [2; 6])
    connection.Add(2, [0; 1; 8])
    connection.Add(3, [4; 5; 8])
    connection.Add(4, [3])
    connection.Add(5, [0; 3])
    connection.Add(6, [1])
    connection.Add(7, [])
    connection.Add(8, [0; 3])

    let finalState =  [true; true; true; true; true; true; true; false; true]

    let net = Network(computers, connection, osProb)
    let rec loop acc =
        if acc = 50 then net.IsFinished()
        else
            net.DoStep() |> ignore
            loop (acc + 1)
    (loop 0) |> should equal true
    net.Computers |> List.filter (fun comp -> comp.IsInfected <> finalState[comp.Index]) |> should be Empty
    

[<Test>]
let TestWithZeroProb () =
    let osProb = Map [ ("Windows", 0.0); ("Linux", 0.0); ("MacOS", 0.0); ("other", 0.0)]
    let computers = [Computer(0, "Windows", false, osProb);
    Computer(1, "Linux", true, osProb);
    Computer(2, "Linux", false, osProb);
    Computer(3, "MacOS", false, osProb);]
    
    let connection = Dictionary<int, int list>()
    connection.Add(0, [1])
    connection.Add(1, [2; 0])
    connection.Add(2, [3; 1])
    connection.Add(3, [2])
    let finalState =  [false; true; false; false]

    let net = Network(computers, connection, osProb)
    let rec loop acc =
        if acc = 10 then net.IsFinished()
        else
            net.DoStep() |> ignore
            loop (acc + 1)
    (loop 0) |> should equal true
    net.Computers |> List.map (fun comp -> comp.IsInfected) |> should equal finalState

[<Test>]
let TestWithHundredProb () =
    let osProb = Map [ ("Windows", 1.0); ("Linux", 1); ("MacOS", 1); ("other", 1)]
    let computers = [Computer(0, "Windows", false, osProb);
    Computer(1, "Linux", true, osProb);
    Computer(2, "Linux", false, osProb);
    Computer(3, "MacOS", false, osProb);]
    
    let connection = Dictionary<int, int list>()
    connection.Add(0, [1])
    connection.Add(1, [2; 0])
    connection.Add(2, [3; 1])
    connection.Add(3, [2])

    let finalState =  [true; true; true; true]

    let net = Network(computers, connection, osProb)
    net.DoStep() |> ignore
    net.IsFinished() |> should equal true
    net.Computers |> List.filter (fun comp -> comp.IsInfected <> finalState[comp.Index]) |> should be Empty

[<Test>]
let TestWithNoNet () =
    let computers = []
    
    let connection = Dictionary<int, int list>()

    let finalState =  []
    
    let osProb = Map [ ("Windows", 1.0); ("Linux", 0); ("MacOS", 0.5); ("other", 1)]

    let net = Network(computers, connection, osProb)
    net.DoStep() |> ignore
    net.IsFinished() |> should equal true
    net.Computers |> List.filter (fun comp -> comp.IsInfected <> finalState[comp.Index]) |> should be Empty
