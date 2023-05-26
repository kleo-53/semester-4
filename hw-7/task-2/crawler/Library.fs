namespace crawler

open System.Text.RegularExpressions
open System.Net.Http

module Crawler =
    // Pattern to match links with
    let pageLinksPattern = Regex(@"<a href=""?(https://?\S*)""", RegexOptions.Compiled)

    // Finds links in page content
    let findLinks page =
        page |> pageLinksPattern.Matches |> Seq.map (fun i -> i.Groups[1].Value)

    // Gets list og links and sizes by given link
    let Crawler (link: string) =
        async {
            try
                use client = new HttpClient()

                // Returns response bogy of page by link
                let connectAsync (link: string) =
                    async {
                        try
                            let! res = Async.AwaitTask(client.GetStringAsync link)
                            return Some (link, res.Length)
                        with
                            | _ -> return None
                    }

                let tasks page = 
                    (findLinks page) |> Seq.map (fun (link : string) -> (connectAsync link))

                return Some ((tasks link) |> Async.Parallel)
            with 
                | _ -> return None
        }