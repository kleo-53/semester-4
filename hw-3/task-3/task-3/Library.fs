namespace task_3

module LambtaInterpreter =
    // Components of the type Term
    type Term =
        | Variable of char
        | Application of Term * Term
        | Abstraction of char * Term

    // Module of lambda interpreter functions
    module Interpreter =
        // Returns set with all free variables in given term
        let GetFreeVariables lambdaTerm =
            let FreeVarSet = Set.empty
            let rec loop lambdaTerm =
                match lambdaTerm with
                | Variable v -> FreeVarSet.Add v
                | Application (termA,  termB) -> (loop termA) + (loop termB)
                | Abstraction (v, term) -> (FreeVarSet.Remove v) + (loop term)
            loop lambdaTerm

        // Returns name of variable which is not in given set
        let getNewVariable (varSet:Set<char>)=
            let rec loop c =
                if varSet.Contains c then loop (char(int(c) + 1))
                else c
            loop 'a'

        // Performs a substitution fo a term (givenTerm) into a variable (givenVariable) in another term (lambdaTerm)
        let substitute givenVariable givenTerm lambdaTerm =
            let rec loop variable body lTerm =
                match body with 
                | Variable v when v = variable -> lTerm
                | Variable v -> body
                | Application (termA, termB) -> Application (loop variable termA lTerm, loop variable termB lTerm)
                | Abstraction (v, term) when v = variable -> Abstraction (v, term)
                | Abstraction (v, term) when (not ((GetFreeVariables lTerm).Contains v) || not ((GetFreeVariables term).Contains variable)) ->
                    Abstraction (v, loop variable term lTerm)
                | Abstraction (v, term) -> 
                    let newVariable = getNewVariable ((GetFreeVariables term) + (GetFreeVariables lTerm))
                    Abstraction (newVariable, loop variable (loop v term (Variable newVariable)) lambdaTerm)
            loop givenVariable givenTerm lambdaTerm

        // Finds application of lambda abstraction to some term and performs a substitution
        let betaConversion lambdaTerm =
            let rec loop lTerm isFound =
                match lTerm with
                | Variable v -> Variable v, isFound
                | Application (Abstraction (v, term), termB) -> substitute v term termB, true
                | Application (termA, termB) -> 
                    let newTermA, foundA = loop termA isFound
                    let newTermB, foundB = loop termB isFound
                    Application (newTermA, newTermB), (foundA && foundB)
                | Abstraction (v, term) -> 
                    let newTerm, newFound = loop term isFound 
                    Abstraction (v, newTerm), newFound
            loop lambdaTerm false

        // Evaluation of term
        let betaReduction lambdaTerm =
            let rec loop lTerm isFound =
                if not isFound then lTerm
                else
                    let newTerm, newFound = betaConversion lTerm
                    loop newTerm newFound
            loop lambdaTerm true