module Crawler.Tests

open NUnit.Framework
open FsUnit
open crawler

[<Test>]
let CorrectLinksAndSizesFromSeSite () =
    let givenLink = "https://se.math.spbu.ru/practice"
    let correctList = [
        Some ("https://oops.math.spbu.ru/SE/alumni", 49175)
        Some ("https://oops.math.spbu.ru/SE/alumni", 49175)
    ]
    Crawler.Crawler givenLink 
    |> (Async.RunSynchronously) 
    |> Option.get 
    |> (Async.RunSynchronously) 
    |> Seq.toList 
    |> should equal correctList

[<Test>]
let CorrectLinksAndSizesFromMicrosoft () =
    let givenLink = "https://learn.microsoft.com/en-us/windows/win32/dlls/dynamic-link-library-search-order"
    let correctList = [
         Some ("https://learn.microsoft.com/en-us/lifecycle/faq/internet-explorer-microsoft-edge", 62649)
    ]
    Crawler.Crawler givenLink 
    |> (Async.RunSynchronously) 
    |> Option.get 
    |> (Async.RunSynchronously) 
    |> Seq.toList 
    |> should equal correctList


[<Test>]
let CorrectIncorrectLink () =
    let givenLink = "https://le.a..r.n.der"
    let correctList = [
         None
    ]
    Crawler.Crawler givenLink 
    |> (Async.RunSynchronously) 
    |> should equal None