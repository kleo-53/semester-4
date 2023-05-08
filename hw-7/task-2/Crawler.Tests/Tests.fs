module Crawler.Tests

open NUnit.Framework
open FsUnit
open crawler

[<Test>]
let CorrectLinksAndSizesFromSeSite () =
    let givenLink = "https://se.math.spbu.ru/practice"
    let correctList = [
        ("https://oops.math.spbu.ru/SE/alumni", 49175)
        ("https://oops.math.spbu.ru/SE/alumni", 49175)
    ]
    Crawler.Crawler givenLink |> should equal correctList

[<Test>]
let CorrectLinksAndSizesFromMicrosoft () =
    let givenLink = "https://learn.microsoft.com/en-us/windows/win32/dlls/dynamic-link-library-search-order"
    let correctList = [
         ("https://learn.microsoft.com/en-us/lifecycle/faq/internet-explorer-microsoft-edge", 63160)
    ]
    Crawler.Crawler givenLink |> should equal correctList