namespace crawler

open System.Text.RegularExpressions
open System.Net.Http

module Crawler =
    // Pattern to match links with
    let pageLinksPattern = Regex(@"<a href=""?(https://?\S*)""", RegexOptions.Compiled)

    // Finds links in page content
    let findLinks page =
        page |> pageLinksPattern.Matches |> Seq.map (fun i -> i.Groups[1].Value)

    // Returns response bogy of page by link
    let connectAsync (link: string) =
        async {
            try
                use client = new HttpClient()
                return Some (client.GetStringAsync link |> Async.AwaitTask |> Async.RunSynchronously)
            with
                | _ -> return None
        }

    // Gets list og links and sizes by given link
    let Crawler (link: string) =
        match (connectAsync link |> Async.RunSynchronously) with 
        | Some content ->
            let sizes = ((findLinks content) 
            |> Seq.map (fun i -> connectAsync i) 
            |> Async.Parallel 
            |> Async.RunSynchronously 
            |> Seq.map (fun i -> 
                match i with 
                | Some i -> i.Length
                | None -> 0))
            List.zip ((findLinks content) |> List.ofSeq) (sizes |> List.ofSeq)
        | None -> List.zip [link] [0]

    // Prints pages links with their sizes
    let PrintCrawler link =
        (Crawler link) |> List.map (fun (l, s) -> printf $"{l} - {s}")