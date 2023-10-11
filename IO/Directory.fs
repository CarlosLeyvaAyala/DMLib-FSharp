module DMLib.IO.Directory

open System.IO

[<CompiledName "Copy">]
let rec copy recursive source destination =
    let dir = DirectoryInfo(source)

    if not dir.Exists then
        raise (DirectoryNotFoundException $"Source directory not found: {dir.FullName}")

    // Create the destination directory
    Directory.CreateDirectory(destination) |> ignore

    // Get the files in the source directory and copy to the destination directory
    dir.GetFiles()
    |> Array.iter (fun file ->
        Path.Combine(destination, file.Name)
        |> file.CopyTo
        |> ignore)

    // If recursive and copying subdirectories, recursively call this method
    if recursive then
        dir.GetDirectories()
        |> Array.iter (fun subDir ->
            Path.Combine(destination, subDir.Name)
            |> copy true subDir.FullName)
