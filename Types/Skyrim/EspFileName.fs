namespace DMLib.Types.Skyrim

open DMLib.String
open DMLib.IO.Path

type EspFileName =
    | Plugin of string
    static member Create(x) = Plugin x
    member p.Value() = let (Plugin x) = p in x

[<AutoOpen>]
module EspFileNameTopLevelOperations =
    let validExtensions = [ "esm"; "esp"; "esl" ] |> Set.ofList

    let isValidExtension s =
        let e = s |> getExtNoDot |> toLower

        match validExtensions.Contains e with
        | true -> Ok None
        | false -> Error e

    let validate f =
        if (isNullOrEmpty f)
           || (f |> getFileNameWithoutExtension |> isNullOrEmpty) then
            invalidArg (nameof f) "An Skyrim plugin name can not be empty."

        match isValidExtension f with
        | Error e ->
            invalidArg (nameof f) $"File extension \"{e}\" is not valid for a Skyrim plugin. Must be esm, esp or esl."
        | _ -> ()

    let EspFileName fileName =
        validate fileName
        EspFileName.Create(fileName)
