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
    Computer(7, Other, true);
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

    let finalState =  [true; true; true; true; true; true; true; true; true]

    let net = Network(computers, connection)
    while (not (net.IsFinished())) do
        net.DoStep()
        net.PrintState()

    net.Computers |> List.filter (fun comp -> comp.IsInfected <> finalState[comp.Index]) |> should be Empty