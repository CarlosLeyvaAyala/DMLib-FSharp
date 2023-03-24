namespace DMLib

open System.Diagnostics
open DMLib.String
open Microsoft.FSharp.Reflection

module Link =
    let openInBrowser url =
        if isUrl url then
            let mutable p = ProcessStartInfo(url)
            p.UseShellExecute <- true
            Process.Start(p) |> ignore

[<AutoOpen>]
module Patterns =
    /// Checks if some value is the same as other.
    let (|Equals|_|) x y = if x = y then Some() else None

module Objects =
    /// Converts a record to an array so its fields can be iterated, pretty much like Javascript.
    let recordToArray record =
        FSharpType.GetRecordFields(record.GetType())
        |> Array.map (fun v -> v.Name, v.GetValue(record))
