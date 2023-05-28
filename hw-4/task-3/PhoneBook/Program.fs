open System.IO
open Microsoft.FSharp.Collections

type SearchType = 
    | Phone
    | Name

// Type of the phonebook
type PhoneBookType(book: Map<string, string>) = 
    // path to file where to save records
    // Adds record to the phonebook
    member _.AddRecord name (phone: string) =
        if Map.containsKey name book then
            let res = book |> Map.filter (fun n p -> p.Contains phone)
            if (res.IsEmpty) then 
                let newPhonebook = book |> Map.change name (fun x ->
                    match x with
                    | Some s -> Some (s + ", " + phone)
                    | None -> Some (phone)
                )
                newPhonebook
            else
                book
        else
            Map.add name phone book

    // Finds name by given phone or phone by given name
    member _.Find (lineType: SearchType) (line: string) = 
        match lineType with
        | Phone ->
            if (book |> Map.exists (fun n (p: string) -> p.Contains line)) then
                book |> Map.filter(fun n (p: string) -> p.Contains line)
            else
                Map.empty
        | Name ->
            if (book |> Map.exists (fun n p -> n = line)) then
                book |> Map.filter (fun n p -> line = n) |> Map.map (fun n p -> p)
            else
                Map.empty

    // Prints the phonebook to the console
    member _.PrintPhonebook = 
        book |>  Map.iter (fun n p -> printfn $"Name: {n}, phone(s): {p}")

    // Saves records to the file
    member _.SaveToFile path =
        printfn $"{path}"
        use streamWriter = File.CreateText(path)
        for np in book do
            printfn $"{np.Key}"
            streamWriter.WriteLine $"Name: {np.Key}, phone(s): {np.Value}"
        streamWriter.Close

    // Fill current phonebook with records from file 
    member _.ReadFromFile path =
        if (not (File.Exists(path))) then Map.empty
        else
            use streamReader = File.OpenText(path)
            let rec loop (newPhoneBook: Map<string, string>) (line: string) =
                if (line = null) then newPhoneBook
                else
                    let splitLine = line.Split ", phone(s): "
                    let name = splitLine[0].Split "Name: "
                    loop (newPhoneBook.Add(name[1], splitLine[1])) (streamReader.ReadLine())
            let res = loop (Map.empty) (streamReader.ReadLine())
            streamReader.Close |> ignore
            res
    

[<EntryPoint>]
let main _ =
    let phonebook = PhoneBookType(Map [])
    let path = Path.Combine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../..")), "Phonebook.txt")
    let rec loop (phonebook: PhoneBookType) =
        printfn "Select command:"
        printfn "0 - Quit"
        printfn "1 - Add record (name + phone)"
        printfn "2 - Find phone by name"
        printfn "3 - Find name by phone"
        printfn "4 - Print all records"
        printfn "5 - Save current records to file"
        printfn "6 - Read records from file"
        let command = System.Console.ReadLine()
        match command with
        | "0" -> 
            printfn "Quit program"
            ()

        | "1" -> 
            printfn "Enter name: "
            let name = System.Console.ReadLine()
            printfn "Enter phone: "
            let phone = System.Console.ReadLine()
            let newPhonebook = PhoneBookType(phonebook.AddRecord name phone)
            printfn "Add successfully"
            loop newPhonebook

        | "2" ->
            printfn "Enter name: "
            let name = System.Console.ReadLine()
            let tempBook = phonebook.Find Name name
            if (tempBook.IsEmpty) then
                printfn "Person with this name does not exist"
            else
                printfn $"Person {name} has phone(s) {[| for np in tempBook -> np.Value |].[0]}"
            loop phonebook

        | "3" ->
            printfn "Enter phone: "
            let phone = System.Console.ReadLine()
            let tempBook = phonebook.Find Phone phone
            if (tempBook.IsEmpty) then
                printfn "Person with this phone does not exist"
            else
                let names = [| for np in tempBook -> np.Key |]
                if (names.Length = 1) then
                    printfn $"Person with phone {phone} has name {names.[0]}"
                else
                    printfn $"Here are people with phone {phone}:"
                    names |> Seq.iter (printfn "%s")
            loop phonebook

        | "4" -> 
            phonebook.PrintPhonebook
            loop phonebook

        | "5" -> 
            phonebook.SaveToFile path |> ignore
            loop phonebook

        | "6" ->
            loop (PhoneBookType(phonebook.ReadFromFile path))

        | _ -> 
            printfn "Invalid command!"
            loop phonebook

    loop phonebook
    0




(* модуль книги со словарем человек : телефоны,
main со всей работой с пользователем
выйти;
добавить запись (имя и телефон);
найти телефон по имени;
найти имя по телефону;
вывести всё текущее содержимое базы;
сохранить текущие данные в файл;
считать данные из файла.
*)