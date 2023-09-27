// Generates the list of files of this project relative to an fsx file.
//
// Needed to create this because my last update of Visual Studio completelly
// fucked up my nuget configuration and I can't use
//          #r "nuget: carlos.leyva.ayala.dmlib"
// anymore.

open System.IO
open System

#r "nuget: TextCopy"
#load "Result.fs"
#load "String.fs"

let target =
    @"C:\Users\Osrail\Documents\GitHub\Armor-Keyword-Manager\Data\scripts\Scratchpad.fsx"

/////////////////////////////////////////////////////////////////////
// Don't modify anything below
/////////////////////////////////////////////////////////////////////
let (|Regex|_|) pattern input =
    let m = System.Text.RegularExpressions.Regex.Match(input, pattern)

    if m.Success then
        Some(List.tail [ for g in m.Groups -> g.Value ])
    else
        None

let smartNl =
    let isNullOrEmpty = String.IsNullOrEmpty

    let smartFold separator acc s =
        if isNullOrEmpty acc then
            s
        else
            acc + separator + s

    smartFold "\n"

let copyDeclarations target =
    let absPath (s: string) = Path.Combine(__SOURCE_DIRECTORY__, s)

    File.ReadAllLines(absPath "DMLib-FSharp.fsproj")
    |> Array.choose (function
        | Regex "<Compile Include=\"(.*\\.fs)\"" [ fn ] -> fn |> absPath |> Some
        | _ -> None) // Get file names
    |> Array.map (fun fn ->
        let u1 = Uri(fn)
        let u2 = Uri(target)

        let path =
            (u2.MakeRelativeUri u1)
                .OriginalString.Replace("/", "\\")

        $"#load \"{path}\"") // Get declarations
    |> Array.insertAt 0 "// DMLib includes must be deleted once nuget works again"
    |> Array.fold smartNl ""
    |> TextCopy.ClipboardService.SetText

copyDeclarations target
