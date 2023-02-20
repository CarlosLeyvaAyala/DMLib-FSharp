module DMLib.IO.Path

open System
open System.IO
open System.Text.RegularExpressions
open DMLib.Combinators

let getDir path = Path.GetDirectoryName(path: string)
let getExt path = Path.GetExtension(path: string)
let getExtNoDot fileName = (getExt fileName)[1..]
let getFileName path = Path.GetFileName(path: string)

let getFileNameWithoutExtension path =
    Path.GetFileNameWithoutExtension(path: string)

let getTempPath () = Path.GetTempPath()
let changeExtension ext path = Path.ChangeExtension(path, ext)

let getRelativeDir relPath dir =
    Path.GetFullPath(Path.Combine(dir, relPath))

let removeDrive path =
    let m = Regex.Match(path, @".*:\\(.*)")

    if m.Success then
        m.Groups[1].Value
    else
        path

let trimEndingDirectorySeparator path =
    Path.TrimEndingDirectorySeparator(path: string)

let combineArray a = Path.Combine(a)
let combine2 p1 p2 = Path.Combine(p1, p2)
/// Pipeable version of Path.Combine
let combine2' = swap combine2
let combine3 p1 p2 p3 = Path.Combine(p1, p2, p3)
let combine4 p1 p2 p3 p4 = Path.Combine(p1, p2, p3, p4)

let changeDirectory (path: string) (newDir) = path |> getFileName |> combine2 newDir


/// <summary>Checks if a file name has any of the extensions on an extension list.</summary>
/// <remarks>Extensions don't start with <c>'.'</c>.</remarks>
/// <param name="extList">String list of extensions to check for. </param>
/// <param name="fileName">File name to check.</param>
/// <returns><c>bool</c></returns>
let isExtensionL extList fileName =
    let ext = getExtNoDot fileName

    let isSameExt e =
        String.Equals(e, ext, StringComparison.OrdinalIgnoreCase)

    (extList |> List.filter isSameExt).Length > 0


let (|IsDir|_|) path =
    match File.GetAttributes(path: string) with
    | FileAttributes.Directory -> Some path
    | _ -> None


let (|FileExists|_|) input =
    if File.Exists(input) then
        Some input
    else
        None

/// Does a file end with an extension? Extension to check does not need to start with <c>'.'</c>.
let (|IsExtension|_|) (ext: string) input =
    let ext' =
        if ext.StartsWith('.') then
            ext
        else
            "." + ext

    if String.Equals(ext', getExt input, StringComparison.OrdinalIgnoreCase) then
        Some input
    else
        None
