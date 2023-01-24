module DMLib.Files

open System.IO
open System.Text.Json

/// Returns file contents as string if it exists. Shouldn't be used in C#.
let fileToStr fileName =
    if not (File.Exists(fileName)) then
        None
    else
        Some(File.ReadAllText(fileName))

/// Returns a file as string if it exists.
let FileToStr fileName =
    match fileToStr fileName with
    | None -> ""
    | Some str -> str

/// Saves some object to a Json text file.
let JsonSerializeTxt fileName obj =
    let str = JsonSerializer.Serialize(obj)
    File.WriteAllText(fileName, str)
