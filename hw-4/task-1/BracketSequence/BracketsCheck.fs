let closingBrackets() = Map [ (')', '('); (']', '['); ('}', '{') ]
let openingBrackets() = Set ( seq {'('; '['; '{' } )

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
            let character = sequence[i]
            if (openingBrackets() |> Set.contains character) then
                loop (i + 1) (sequence[i] :: stack) continueLooping
            elif (closingBrackets() |> Map.containsKey character) then
                match stack with
                | [] -> loop (i + 1) stack false
                | head :: tail -> 
                     match head with
                     | bracket when (closingBrackets() |> Map.find character) = bracket -> loop (i + 1) tail continueLooping
                     | _ -> loop (i + 1) tail false
            else
                loop (i + 1) stack continueLooping 
    loop 0 stack true