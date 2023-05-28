module Network
open System
open System.Collections.Generic


//let mutable prob = Map [ ("winProb", 0.7); ("linProb", 0.4); ("macProb", 0.5); ("otherProb", 0.9)]
let getProbability (os:string, probs:Map<string, float>) =
    match (probs.TryGetValue os) with 
    | (res, prob) when res = true -> prob
    | _ -> 1

type Computer(index: int, os: string, hasVirus: bool, osProbs:Map<string, float>) =
        let mutable HasVirus = hasVirus

        let tryInfect (osProbs:Map<string, float>) =
            let rnd = (Random().Next(0, 100):float) / 100.0
            let prob = getProbability(os, osProbs)
            if (prob <> 0 && rnd <= prob && not HasVirus) then 
                HasVirus <- true

        member _.OS = os
        member _.Print = $"%A{os}: %b{HasVirus} "
        member _.TryInfect = tryInfect(osProbs)
        member _.IsInfected = HasVirus
        member _.Index = index
    
        new(index, os: string, osProbs) = Computer(index, os, false, osProbs)

 type Network (computers:Computer list, connection: Dictionary<int, int list>, OSProbs: Map<string, float>) =
    let osProbs = OSProbs
    
    let mutable isFinished = false

    let infect (baseComp: Computer) = 
        if baseComp.IsInfected then
            connection.[baseComp.Index] |> 
            List.map (fun i -> (computers[i].TryInfect)) |> ignore

            
    let checkComp(baseComp: Computer) = 
        if baseComp.IsInfected then
            connection.[baseComp.Index] |> 
            List.filter (fun i -> (not computers[i].IsInfected && getProbability(computers[i].OS, osProbs) <> 0)) = list.Empty
        else 
            true

    let CheckFinish() = 
        if not isFinished then 
            isFinished <- computers |> List.filter (fun comp -> (checkComp comp) = false) = list.Empty
        isFinished

    member _.DoStep () = 
        computers |> List.map (fun comp -> infect comp) |> ignore

    member _.IsFinished () = 
        CheckFinish()

    member _.PrintState () = 
        computers |> List.iter (fun comp -> printf $"{comp.Print}")
        printf "\n"

    member _.Computers = computers
            