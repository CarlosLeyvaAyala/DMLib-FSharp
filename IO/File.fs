namespace DMLib.IO

open System.Diagnostics

module File =
    let execute url =
        let mutable p = ProcessStartInfo(url)
        p.UseShellExecute <- true
        Process.Start(p) |> ignore
