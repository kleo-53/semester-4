module PointFree.Tests

open NUnit.Framework
open Program
open FsCheck

[<Test>]
let Test0and1Func () =
    let checking (x:int) (l:list<int>) = (func x l) = (func1 x l) 
    Check.QuickThrowOnFailure checking 

[<Test>]
let Test0and2Func () =
    let checking (x:int) (l:list<int>) = (func x l) = (func2 x l) 
    Check.QuickThrowOnFailure checking

[<Test>]
let Test0and3Func () =
    let checking (x:int) (l:list<int>) = (func x l) = (func3 x l) 
    Check.QuickThrowOnFailure checking 