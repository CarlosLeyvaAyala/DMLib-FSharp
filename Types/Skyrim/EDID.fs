namespace DMLib.Types.Skyrim

open System
open System.Text.RegularExpressions
open DMLib.String

[<Sealed>]
type EDID(edid: string) =
    let illegalChars =
        [ "white-space", @"\s"
          "~", "~"
          "|", @"\|"
          ",", "," ]

    let (|HasIllegalChars|_|) s =
        let wrong =
            illegalChars
            |> List.map (fun (display, r) -> Regex(r).Match(s).Success, display)
            |> List.filter (fun (found, _) -> found)

        match wrong with
        | [] -> None
        | (_, displayedChar) :: _ -> Some(displayedChar)

    let validate s =
        match s with
        | IsEmptyStr ->
            "An empty string can not be an EDID."
            |> invalidArg (nameof s)
        | HasIllegalChars d ->
            sprintf "\"%s\" is an invalid EDID. The \"%s\" character is not allowed." s d
            |> invalidArg (nameof s)
        | _ -> ()

        s

    let v = validate edid
    member e.Value = v
    override w.GetHashCode() = v.ToString() |> hash
    override w.ToString() = sprintf "EDID %s" v

    override s.Equals(a) =
        match a with
        | :? EDID as x -> x.Value = s.Value
        | _ -> false

    interface System.IComparable with
        member s.CompareTo a =
            match a with
            | :? EDID as x -> compare (s.Value.ToLower()) (x.Value.ToLower())
            | _ -> invalidArg (nameof a) "Can not compare to this type."

[<AutoOpen>]
module EDIDConstructor =
#if INTERACTIVE
    fsi.AddPrinter(fun (r: EDID) -> r.ToString())
#endif
    let EDID s = EDID(s)
