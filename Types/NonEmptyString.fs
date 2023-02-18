namespace DMLib.Types

open System
open DMLib.String

[<Sealed>]
type NonEmptyString(s: string) =
    let validated =
        match s with
        | IsEmptyStr -> invalidArg (nameof s) $"This string can not be empty."
        | IsWhiteSpaceStr -> invalidArg (nameof s) $"This string can not be white-spaces only."
        | _ -> ()

        s

    let v = validated

    member s.Value = v
    override s.GetHashCode() = s.ToString() |> hash
    override s.ToString() = sprintf "NonEmpty %s" v

    override s.Equals(a) =
        match a with
        | :? NonEmptyString as x -> x.Value = s.Value
        | _ -> false

    interface System.IComparable with
        member s.CompareTo a =
            match a with
            | :? NonEmptyString as x -> compare s.Value x.Value
            | :? string as x -> compare s.Value x
            | _ -> invalidArg (nameof a) "Can not compare to this type."

[<AutoOpen>]
module NonEmptyStringConstructor =
#if INTERACTIVE
    fsi.AddPrinter(fun (r: NonEmptyString) -> r.ToString())
#endif
    let inline NonEmptyString (s: string) = NonEmptyString(s)
