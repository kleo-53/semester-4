module task_3.Tests

open NUnit.Framework
open task_3.LambtaInterpreter
open LambtaInterpreter.Interpreter
open FsUnit

let I () = Abstraction('x', Variable 'x')

let S () = Abstraction('s', Application(Variable 's', Variable 's'))

let K () = Abstraction('x', Abstraction('y', Variable 'x'))

let KStar () = Abstraction('y', Abstraction('x', Variable 'x'))

//(λx.λy.x y t) λu.v
let BetaReductionTest () =
        Application(
            Abstraction(
                'x',
                Abstraction(
                    'y',
                    Application(Application(Variable 'x', Variable 'y'), Variable 't')
                )
            ),
            Abstraction('u', Variable 'v')
        )

// λy.vt
let BetaReductionAnswer () =
            Abstraction ('y', Application (Variable 'v', Variable 't'))

//(λx.(λy.x) a) b
let SecondElementTest () = Application(
                        Abstraction('x', 
                        Application(
                            Abstraction('y',
                                Variable 'x'), Variable 'a')), Variable 'b')
// b
let SecondElementResult () = Variable 'b'

[<Test>]
let ``Both abstraction ans application test`` () =  
     betaReduction (BetaReductionTest()) |> should equal (BetaReductionAnswer()) 

[<Test>]
let ``Finds first element from pair`` () =  
     betaReduction (SecondElementTest()) |> should equal (SecondElementResult()) 

[<Test>]
let ``I I = I`` () =
    betaReduction (Application (I(), I())) |> should equal (I())

[<Test>]
let ``K I = KStar`` () =
    betaReduction (Application (K(), I())) |> should equal (KStar())

[<Test>]
let ``S I = I`` () =
    betaReduction (Application(S (), (I())))
    |> should equal (I())