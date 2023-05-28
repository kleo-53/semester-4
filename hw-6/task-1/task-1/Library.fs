namespace task_1
open System

module RoundingWorkflow =
    // Workflow which makes calculations with given accuracy
    type Builder (n:int) =
        member this.Bind(x:float, f) =
            Math.Round(x, n) |> f

        member this.Return(x:float) = Math.Round(x, n)