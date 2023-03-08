namespace DMLib.Types.Skyrim

open DMLib.String
open DMLib.IO.Path

[<Sealed>]
type EspFileName(filename: string) =
    static let (|IsInvalidExtension|_|) s =
        let validExtensions = [ "esm"; "esp"; "esl" ] |> Set.ofList
        let ext = s |> getExtNoDot |> toLower

        match validExtensions.Contains ext with
        | true -> None
        | false -> Some()

    static let (|FileNameIsEmpty|_|) f =
        let f' =
            f
            |> getFileNameWithoutExtension
            |> isNullOrWhiteSpace

        match f' with
        | false -> None
        | true -> Some()

    static let validate f =
        match f with
        | IsWhiteSpaceStr
        | FileNameIsEmpty -> invalidArg (nameof f) "An Skyrim plugin name can not be empty."

        | IsInvalidExtension ->
            let m =
                $"File extension \"{f}\" is not valid for a Skyrim plugin. Must be esm, esp or esl."

            invalidArg (nameof f) m

        | _ -> ()

        f

    let v = validate filename
    member s.Value = v

    override s.ToString() = sprintf "SkyrimPlugin %s" v
    override s.GetHashCode() = s.ToString() |> hash

    override s.Equals(a) =
        match a with
        | :? EspFileName as x -> x.Value = s.Value
        | _ -> false

    interface System.IComparable with
        member s.CompareTo a =
            match a with
            | :? EspFileName as x -> compare (s.Value.ToLower()) (x.Value.ToLower())
            | _ -> invalidArg (nameof a) "Can not compare to this type."

[<AutoOpen>]
module EspFileNameConstructor =
#if INTERACTIVE
    fsi.AddPrinter(fun (r: EspFileName) -> r.ToString())
#endif
    let EspFileName fileName = EspFileName(fileName)
