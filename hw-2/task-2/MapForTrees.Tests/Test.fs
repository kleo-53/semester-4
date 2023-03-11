module MapForTrees.Tests

open NUnit.Framework
open FsUnit
open MapForTree

let leftTree = Tree.Tree(1, Tree.Tip 2, Tree.Tip 3)
let tree = Tree.Tree(20, leftTree, Tree.Tip 0)
let correctLeftTree = Tree.Tree(-3, Tree.Tip -2, Tree.Tip -1)
let correctTree = Tree.Tree(40, Tree.Tree(2, Tree.Tip 4, Tree.Tip 6), Tree.Tip 0)
let func x = x * 2
let funcMinusFour x = x - 4

[<Test>]
let TreeTest () =
    mapForTree funcMinusFour leftTree |> should equal correctLeftTree
    mapForTree func tree |> should equal correctTree