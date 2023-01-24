module DMLib.Link

open System.Diagnostics
open System.Text.RegularExpressions
open DMLib.String

let openInBrowser url =
    if isUrl url then
        let mutable p = ProcessStartInfo(url)
        p.UseShellExecute <- true
        Process.Start(p) |> ignore
