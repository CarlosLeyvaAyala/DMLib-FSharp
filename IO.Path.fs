module DMLib.IO.Path

open System
open System.IO
open DMLib.String
open DMLib.Combinators

let getDir path = Path.GetDirectoryName(path: string)
let getExt path = Path.GetExtension(path: string)
let getExtNoDot fileName = (getExt fileName)[1..]
let getFileName path = Path.GetFileName(path: string)

let getFileNameWithoutExtension path =
    Path.GetFileNameWithoutExtension(path: string)

let getTempPath () = Path.GetTempPath()
let changeExtension ext path = Path.ChangeExtension(path, ext)
let changeExt = changeExtension

let getRelativeDir relPath dir =
    Path.GetFullPath(Path.Combine(dir, relPath))

let removeDrive path =
    match path with
    | Regex @".*:\\(.*)" [ np ] -> np
    | p -> p

let trimEndingDirectorySeparator path =
    Path.TrimEndingDirectorySeparator(path: string)

let combineArray a = Path.Combine(a)
/// Pipeable version of Path.Combine
let combine2 p1 p2 = Path.Combine(p1, p2)
let combine2' = swap combine2
let combine3 p1 p2 p3 = Path.Combine(p1, p2, p3)
let combine4 p1 p2 p3 p4 = Path.Combine(p1, p2, p3, p4)

/// Changes the directory of a file name while maintaining the file name.
let changeDirectory (path: string) (newDir) = path |> getFileName |> combine2 newDir

/// Changes the name of a file while maintaining the directory.
let rename newName (oldName: string) =
    oldName
    |> Path.GetDirectoryName
    |> combine2' newName

/// Changes the name of a file while maintaining the directory.
let rename' = swap rename

/// Changes the name of a file while maintaining the directory. The mapping function accepts the orignal file name.
let renameMap mapping (oldName: string) =
    Path.GetFileName oldName
    |> mapping
    |> combine2 (Path.GetDirectoryName oldName)

/// Changes the name of a file while maintaining the directory. The mapping function accepts the orignal file name.
let renameMap' = swap renameMap

/// Forces a directory into existance.
let forceDir (d: string) =
    if not (Directory.Exists d) then
        Directory.CreateDirectory d |> ignore


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
