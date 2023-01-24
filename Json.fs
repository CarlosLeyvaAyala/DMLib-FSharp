[<RequireQualifiedAccess>]
module DMLib.Json

open System.IO
open System.Text.Json

let deserialize<'a> (str: string) = str |> JsonSerializer.Deserialize<'a>

let getFromFile<'a> path =
    File.ReadAllText(path) |> deserialize<'a>

let serialize indented obj =
    JsonSerializer.Serialize(obj, JsonSerializerOptions(WriteIndented = indented))

let writeToFile indented path obj =
    File.WriteAllText(path, serialize indented obj)
