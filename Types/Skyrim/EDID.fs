namespace DMLib.Types.Skyrim

open System
open System.Text.RegularExpressions

type EDID =
    | EDID of string
    static member Create(edid) = EDID edid
    member e.Value() = let (EDID s) = e in s

[<AutoOpen>]
module EDIDConstructor =
    let illegalChars =
        [ "white-space", @"\s"
          "~", "~"
          "|", @"\|"
          ",", "," ]

    let validate s =
        if String.IsNullOrWhiteSpace s then
            "An empty string can not be an EDID."
            |> invalidArg (nameof s)

        let wrong =
            illegalChars
            |> List.map (fun (display, r) -> Regex(r).Match(s).Success, display)
            |> List.filter (fun (b, d) -> b)

        match wrong with
        | [] -> ()
        | (_, d) :: _ ->
            sprintf "\"%s\" is an invalid EDID. The \"%s\" character is not allowed." s d
            |> invalidArg (nameof s)

        s

    let EDID s = s |> validate |> EDID.Create
