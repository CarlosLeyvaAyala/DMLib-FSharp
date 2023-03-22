namespace DMLib

open System.Diagnostics
open DMLib.String

module Link =
    let openInBrowser url =
        if isUrl url then
            let mutable p = ProcessStartInfo(url)
            p.UseShellExecute <- true
            Process.Start(p) |> ignore

module Patterns =
    /// Checks if some value is the same as other.
    let (|Equals|_|) x y = if x = y then Some() else None
