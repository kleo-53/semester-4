namespace task_2

open System

module StringWorkflow =
    // Checks if string value is a number
    let TryParseString (str: string) =
        try Some (Int32.Parse(str)) with
        | :? System.ArgumentNullException -> None
        | :? System.FormatException -> None
        | :? System.OverflowException -> None

    // Workflow which makes calculations with given accuracy
    type calculate () =
        member this.Bind(x, f) =
            match (TryParseString x) with
            | Some x -> f x
            | None -> None

        member this.Return(x) =
            Some x

