// Matches a paired bracket
let PairToBracket bracket =
    match bracket with 
    | ')' -> '('
    | ']' -> '['
    | '}' -> '{'

// Checks if brackets sequence is correct
let Check (sequence : string) =
    let stack = List.Empty
    let rec loop i (stack : List<char>) continueLooping =
        if (not continueLooping) then false
        elif (i = sequence.Length) then 
            match stack with
            | [] -> true
            | head :: tail -> false
        else 
        match sequence[i] with
        | '(' | '[' | '{' -> loop (i + 1) (sequence[i] :: stack) continueLooping
        | ')' | ']' | '}' ->
            match stack with
            | [] -> loop (i + 1) stack false
            | head :: tail ->
                match head with
                | bracket when (PairToBracket sequence[i]) = bracket -> loop (i + 1) tail continueLooping
                | _ -> loop (i + 1) tail false
        | _ -> loop (i + 1) stack continueLooping
    loop 0 stack true