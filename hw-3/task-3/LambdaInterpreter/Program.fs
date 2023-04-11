type Term =
    | Variable of char
    | Application of Term * Term
    | Abstraction of char * Term

module Interpreter =
    let GetFreeVariables lambdaTerm =
        let FreeVarSet = Set.empty
        let rec loop lambdaTerm =
            match lambdaTerm with
            | Variable v -> FreeVarSet.Add v
            | Application (termA,  termB) -> (loop termA) + (loop termB)
            | Abstraction (v, term) -> (FreeVarSet.Remove v) + (loop term)
        loop lambdaTerm

    let betaReduction =

