namespace DMLib.Types

open System.Text.RegularExpressions
open DMLib.String
open System

[<CustomEquality; CustomComparison>]
type HexNumber =
    | H of string
    static member Create(x) = H x

    member h.ToInt() =
        let (H n) = h in Int32.Parse(n, Globalization.NumberStyles.HexNumber)

    member h.ToInt64() =
        let (H n) = h in Int64.Parse(n, Globalization.NumberStyles.HexNumber)

    override h.ToString() = let (H x) = h in sprintf "Hex %s" x
    override h.GetHashCode() = h.ToString() |> hash

    override h.Equals(a) =
        match a with
        | :? HexNumber as x -> x.ToInt64() = h.ToInt64()
        | _ -> false

    interface System.IComparable with
        member h.CompareTo a =
            match a with
            | :? HexNumber as x -> compare (h.ToInt64()) (x.ToInt64())
            | :? int as x -> compare (h.ToInt64()) x
            | :? int64 as x -> compare (h.ToInt64()) x
            | _ -> invalidArg (nameof a) "Can not compare arguments of different types."

[<AutoOpen>]
module HexNumberTopLevelOperations =
    // Constructor
    let inline HexNumber (s: obj) =
        let v =
            match s with
            | :? int as x -> x.ToString("X")
            | :? int64 as x -> x.ToString("X")
            | :? string as s ->
                if isNullOrEmpty s then
                    invalidArg (nameof s) $"An hex number can not be an empty string."

                let getValue s =
                    let non0x = Regex("(0[xX])?(.*)").Match(s).Groups[2].Value

                    if Regex("[^a-fA-F0-9]").Match(non0x).Success then
                        invalidArg (nameof s) $"{s} is not a hex number."

                    non0x

                getValue s

            | _ -> invalidArg (nameof s) $"Only int, int64 and string can be converted to HexNumber"


        HexNumber.Create(v.ToLower())
