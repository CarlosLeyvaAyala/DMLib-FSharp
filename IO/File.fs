module DMLib.IO.File

open System.Diagnostics
open DMLib.TupleCommon
open DMLib
open System.IO
open DMLib.Combinators
open DMLib.IO.Path

[<CompiledName("Execute")>]
let execute filepath =
    let mutable p = ProcessStartInfo(filepath)
    p.UseShellExecute <- true
    Process.Start(p) |> ignore

[<CompiledName("CopyWithSameName")>]
let copyWithSameName destDir filename =
    filename
    |> dupFst
    |> Tuple.mapSnd (swap changeDirectory destDir)
    |> File.Copy

/// Reads a file. Returns None if file doesn't exist or raises an exception.
///
/// Don't use in C#.
let readAllLines filename =
    try
        File.ReadAllLines filename |> Some
    with
    | _ -> None
