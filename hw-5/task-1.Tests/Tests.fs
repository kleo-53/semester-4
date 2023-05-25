module task_1.Tests

open NUnit.Framework
open System.Collections.Generic
open Network
open FsUnit

[<Test>]
let Test1 () =
    let computers = [Computer(0, Windows, false);
    Computer(1, Linux, true);
    Computer(2, Linux, false);
    Computer(3, MacOS, false);
    Computer(4, Windows, false);
    Computer(5, MacOS, false);
    Computer(6, Linux, true);
    Computer(7, Other, false);
    Computer(8, Other, false)]
    
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

    let net = Network(computers, connection)
    let rec loop acc =
        if acc = 50 then net.IsFinished()
        else
            net.DoStep() |> ignore
            loop (acc + 1)
    (loop 0) |> should equal true
    net.Computers |> List.filter (fun comp -> comp.IsInfected <> finalState[comp.Index]) |> should be Empty
    

[<Test>]
let TestWithZeroProb () =
    let computers = [Computer(0, Windows, false);
    Computer(1, Linux, true);
    Computer(2, Linux, false);
    Computer(3, MacOS, false);]
    
    let connection = Dictionary<int, int list>()
    connection.Add(0, [1])
    connection.Add(1, [2; 0])
    connection.Add(2, [3; 1])
    connection.Add(3, [2])

    let finalState =  [false; true; false; false]

    let net = Network(computers, connection)
    net.SetProbability(0, 0, 0, 0)
    let rec loop acc =
        if acc = 10 then net.IsFinished()
        else
            net.DoStep() |> ignore
            loop (acc + 1)
    (loop 0) |> should equal true
    net.Computers |> List.filter (fun comp -> comp.IsInfected <> finalState[comp.Index]) |> should be Empty

[<Test>]
let TestWithHundredProb () =
    let computers = [Computer(0, Windows, false);
    Computer(1, Linux, true);
    Computer(2, Linux, false);
    Computer(3, MacOS, false);]
    
    let connection = Dictionary<int, int list>()
    connection.Add(0, [1])
    connection.Add(1, [2; 0])
    connection.Add(2, [3; 1])
    connection.Add(3, [2])

    let finalState =  [true; true; true; true]

    let net = Network(computers, connection)
    net.SetProbability(1, 1, 1, 1)
    net.DoStep() |> ignore
    net.IsFinished() |> should equal true
    net.Computers |> List.filter (fun comp -> comp.IsInfected <> finalState[comp.Index]) |> should be Empty

[<Test>]
let TestWithNoNet () =
    let computers = []
    
    let connection = Dictionary<int, int list>()

    let finalState =  []

    let net = Network(computers, connection)
    net.DoStep() |> ignore
    net.IsFinished() |> should equal true
    net.Computers |> List.filter (fun comp -> comp.IsInfected <> finalState[comp.Index]) |> should be Empty
