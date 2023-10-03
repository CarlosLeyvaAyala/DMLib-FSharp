namespace DMLib.IO

open System
open System.IO
open DMLib.String
open DMLib.Combinators

module Path =
    let getDir path = Path.GetDirectoryName(path: string)
    let getExt path = Path.GetExtension(path: string)
    let getExtNoDot fileName = (getExt fileName)[1..]
    let getFileName path = Path.GetFileName(path: string)

    let getFileNameWithoutExtension path =
        Path.GetFileNameWithoutExtension(path: string)

    let getTempPath () = Path.GetTempPath()
    let changeExtension ext path = Path.ChangeExtension(path, ext)
    let changeExt = changeExtension

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

    /// Converts "..\..\xxx" to an absolute path.
    let getRelativeDir relPath dir =
        Path.GetFullPath(Path.Combine(dir, relPath))

    /// Given two absolute file paths, calculates how they are relative to each other.
    let getRelativePath targetPath filepath =
        let u1 = Uri(filepath)
        let u2 = Uri(targetPath)

        (u2.MakeRelativeUri u1)
            .OriginalString.Replace("/", "\\")

    /// Given an absolute and a relative path, expands the relative one.
    let getExpandedPath (targetRelPath: string) (sourceAbsPath: string) =
        let src =
            if Path.HasExtension sourceAbsPath then
                getDir sourceAbsPath
            else
                sourceAbsPath

        let (target, targetFile) =
            if Path.HasExtension targetRelPath then
                getDir targetRelPath, getFileName targetRelPath
            else
                targetRelPath, ""

        src
        |> getRelativeDir target
        |> combine2' targetFile

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

    let getScriptLoadDeclaration sdir sfile filename =
#if INTERACTIVE
        filename
        |> getRelativePath (combine2 sdir sfile)
        |> sprintf "#load \"%s\""
#else
        failwith "This function only works in F# Interactive"
#endif

    let getScriptLoadDeclarations scriptPath projectFile getFilesFrom =
        let projectDir = getDir projectFile

        let filesInProject =
            projectFile
            |> File.ReadAllLines
            |> Array.choose (function
                | Regex "<Compile Include=\"(.*\\.fs)\"" [ fn ] -> fn |> Some
                | _ -> None) // Get file names
            |> Array.map (fun s -> combine2 projectDir s)

        let filesInDir =
            Directory.GetFiles(getFilesFrom, "*.*", SearchOption.AllDirectories)

        filesInProject
        |> Array.filter (fun projectFile ->
            filesInDir
            |> Array.exists (fun dir -> dir = projectFile))
        |> Array.map (fun fn ->
            fn
            |> getRelativePath scriptPath
            |> sprintf "#load \"%s\"")
        |> Array.fold foldNl ""

[<AutoOpen>]
module PathPatterns =
    open Path

    /// <summary>Is the path a directory?</summary>
    /// <returns>The full input file name.</returns>
    let (|IsDir|_|) path =
        match File.GetAttributes(path: string) with
        | FileAttributes.Directory -> Some path
        | _ -> None

    /// <summary>Does a file exist?</summary>
    /// <returns>The full input file name.</returns>
    let (|FileExists|_|) filename =
        if File.Exists filename then
            Some filename
        else
            None

    /// <summary>Does a file exist?</summary>
    /// <returns>The full input file name.</returns>
    let (|FileNotExists|_|) filename =
        if File.Exists filename then
            None
        else
            Some filename

    /// <summary>Does a file end with an extension? Extension to check does not need to start with <c>'.'</c>.</summary>
    /// <returns>The full input file name.</returns>
    let (|IsExtension|_|) (ext: string) filename =
        let ext' =
            if ext.StartsWith('.') then
                ext
            else
                "." + ext

        if String.Equals(ext', getExt filename, StringComparison.OrdinalIgnoreCase) then
            Some filename
        else
            None

    /// <summary>Does a file end with an extension? Extension to check does not need to start with <c>'.'</c>.</summary>
    /// <returns>The full input file name.</returns>
    let (|IsInExtensionList|_|) ext filename =
        if isExtensionL ext filename then
            Some filename
        else
            None
