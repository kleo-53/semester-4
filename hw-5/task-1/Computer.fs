module Network
open System
open System.Collections.Generic

type OperationSystem =
    | Windows
    | Linux 
    | MacOS
    | Other

let mutable prob = Map [ ("winProb", 0.7); ("linProb", 0.4); ("macProb", 0.5); ("otherProb", 0.9)]

let setProb(win, lin, mac, oth) =
    Map [("winProb", win); ("linProb", lin); ("macProb", mac); ("otherProb", oth)]

let getProbability (os:OperationSystem) =
    match os with
    | Windows -> prob["winProb"]
    | Linux -> prob["linProb"]
    | MacOS -> prob["macProb"]
    | _ -> prob["otherProb"]

type Computer(index: int, os: OperationSystem, hasVirus: bool) =
    let mutable HasVirus = hasVirus

    let tryInfect () =
        let rnd = (Random().Next(0, 100):float) / 100.0
        let prob = getProbability os
        if (prob <> 0 && rnd <= prob && not HasVirus) then 
            HasVirus <- true

    member _.OS = os
    member _.Print = $"%A{os}: %b{HasVirus} "
    member _.TryInfect = tryInfect()
    member _.IsInfected = HasVirus
    member _.Index = index
    
    new(index, os: OperationSystem) = Computer(index, os, false)

 type Network (computers:Computer list, connection: Dictionary<int, int list>) =
    let mutable isFinished = false
    let infect (baseComp: Computer) = 
        if baseComp.IsInfected then
            connection.[baseComp.Index] |> 
            List.map (fun i -> (computers[i].TryInfect)) |> ignore

            
    let checkComp(baseComp: Computer) = 
        if baseComp.IsInfected then
            connection.[baseComp.Index] |> 
            List.filter (fun i -> (not computers[i].IsInfected && getProbability(computers[i].OS) <> 0)) = list.Empty
        else 
            true

    let CheckFinish() = 
        if not isFinished then 
            isFinished <- computers |> List.filter (fun comp -> (checkComp comp) = false) = list.Empty
        isFinished

    member _.DoStep () = 
        computers |> List.map (fun comp -> infect comp) |> ignore
        
    member _.SetProbability(win, lin, mac, oth) =
        prob <- setProb(win, lin, mac, oth)

    member _.IsFinished () = 
        CheckFinish()

    member _.PrintState () = 
        computers |> List.iter (fun comp -> printf $"{comp.Print}")
        printf "\n"

    member _.Computers = computers
            