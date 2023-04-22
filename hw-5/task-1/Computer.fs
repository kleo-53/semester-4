module Network
open System
open System.Collections.Generic

type OperationSystem =
    | Windows
    | Linux 
    | MacOS
    | Other

let winProb = 0.7
let linProb = 0.4
let macProb = 0.5
let otherProb = 0.9

let getProbability (os:OperationSystem) =
    match os with
    | Windows -> winProb
    | Linux -> linProb
    | MacOS -> macProb
    | _ -> otherProb

type Computer(index: int, os: OperationSystem, hasVirus: bool) =
    let mutable HasVirus = hasVirus

    let tryInfect () =
        let rnd = (Random().Next(0, 100):float) / 100.0
        if (rnd >= (getProbability os) && not HasVirus) then 
            HasVirus <- true

    member _.OS = os
    member _.Print = $"%A{os}: %b{HasVirus} "
    member _.TryInfect = tryInfect()
    member _.IsInfected = HasVirus
    member _.Index = index
    
    new(index, os: OperationSystem) = Computer(index, os, false)

 type Network (computers:Computer list, connection: Dictionary<int, int list>) =
    let infect (baseComp: Computer) = 
        if baseComp.IsInfected then
            connection.[baseComp.Index] |> List.map (fun i -> computers[i].TryInfect) |> ignore

    member _.DoStep () = computers |> List.map (fun comp -> infect comp) |> ignore
    member _.IsFinished () = 
        (computers |> List.filter (fun comp -> comp.IsInfected = false)) = list.Empty
    member _.PrintState () = 
        computers |> List.iter (fun comp -> printf $"{comp.Print}")
        printf "\n"
    member _.Computers = computers
            