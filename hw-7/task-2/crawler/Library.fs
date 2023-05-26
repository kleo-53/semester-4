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
        let client = new HttpClient()
        async {
            try
                // Returns link and size of response bogy by link
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
                
                let! mainPage = (client.GetStringAsync link) |> Async.AwaitTask

                return Some ((tasks mainPage) |> Async.Parallel)
            with 
                | _ -> return None
        }