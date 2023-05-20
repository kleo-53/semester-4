module ParsingTree.Tests

open NUnit.Framework
open FsCheck
open FsUnit
open ParseTree

let zeroTree = Tree.Tree(Operation.Division, Tree.Tip(132), Tree.Tip(0))
let addTree = Tree.Tree(Operation.Addition, Tree.Tip(3), Tree.Tip(5))
let subTree = Tree.Tree(Operation.Subtraction, Tree.Tip(-4), Tree.Tip(10))
let mulTree = Tree.Tree(Operation.Multiplication, Tree.Tip(5), Tree.Tip(3))
let divTree = Tree.Tree(Operation.Division, Tree.Tip(121), Tree.Tip(11))
let testTree = Tree.Tree(Operation.Addition, Tree.Tree(Operation.Multiplication, addTree, mulTree), Tree.Tree(Operation.Subtraction, subTree, divTree))

let CalculationTestCaseData () =
    [
        addTree, 8.0
        subTree, -14.0
        mulTree, 15.0
        divTree, 11.0
        testTree, 95.0
        zeroTree, infinity
    ] |> List.map (fun (tree, result) -> TestCaseData(tree, result))

[<TestCaseSource(nameof CalculationTestCaseData)>]
let CalculationTest tree correctResult =
    Calculate tree |> should equal correctResult