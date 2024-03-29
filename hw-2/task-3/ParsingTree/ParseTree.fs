﻿module ParseTree
   
type Operation =
    | Addition
    | Subtraction
    | Multiplication
    | Division

// Tree structure
type Tree =
    | Tree of Operation * Tree * Tree
    | Tip of float

// Map function for tree
let rec Calculate tree =
    match tree with
    | Tree(operator, leftTree, rightTree) -> 
        match operator with
        | Addition -> Calculate leftTree + Calculate rightTree
        | Subtraction -> Calculate leftTree - Calculate rightTree
        | Multiplication -> Calculate leftTree * Calculate rightTree
        | Division -> Calculate leftTree / Calculate rightTree 
    | Tip value -> value