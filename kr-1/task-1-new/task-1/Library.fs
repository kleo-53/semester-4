namespace task_1

module superMap =
    let supermap list func =
        let rec loop list acc =
            match list with
            | head :: tail -> loop tail (List.append acc head)
            | [] -> acc
        loop (List.map func list) []
