module PhoneBook.Tests

open NUnit.Framework
open FsUnit
open System.IO
open Program


type Tests () =
    let book = PhoneBookType(Map [])
    let book2 = PhoneBookType(Map ["Anya", "1234567"])
    let records = Map [("Anya", "1234567"); ("Kate", "2233441"); ("Tolik", "1234567, 212120")]
    let bookFull = PhoneBookType(records)

    [<Test>]
    member _.``Adds different records correctly`` () =
        (book.AddRecord "Anya" "1234567") |> should equal (Map.empty.Add("Anya", "1234567"))
        (book2.AddRecord "Kate" "2233441") |> should equal (Map.empty.Add("Anya", "1234567").Add("Kate","2233441"))

    [<Test>]
    member _.``Adds number to same name correctly`` () =
        (book2.AddRecord "Anya" "2233441") |> should equal (Map.empty.Add("Anya", "1234567, 2233441"))

    [<Test>]
    member _.``Finds all phones by name correctly`` () =
        (bookFull.Find "name" "Kate") |> should equal (Map.empty.Add("Kate", "2233441"))
        (bookFull.Find "name" "Tolik") |> should equal (Map.empty.Add("Tolik", "1234567, 212120"))
        (bookFull.Find "name" "Aboba").IsEmpty |> should equal true

    [<Test>]
    member _.``Finds all names by phone correctly`` () =
        (bookFull.Find "phone" "2233441") |> should equal (Map.empty.Add("Kate", "2233441"))
        (bookFull.Find "phone" "1234567") |> should equal (Map.empty.Add("Anya", "1234567").Add("Tolik", "1234567, 212120"))
        (bookFull.Find "phone" "987").IsEmpty |> should equal true

    [<Test>]
    member _.``Saves and reads from file correctly`` () =
        let path = Path.Combine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../..")), "TestRecords.txt")
        bookFull.SaveToFile path |> ignore
        bookFull.ReadFromFile path |> should equal records
