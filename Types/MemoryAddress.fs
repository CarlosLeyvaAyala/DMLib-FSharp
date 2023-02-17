namespace DMLib.Types

open System.Text.RegularExpressions
open DMLib.String
open System

[<CustomEquality; CustomComparison>]
type MemoryAddress =
    | Hex of string
    static member Create(x) = Hex x
    override h.GetHashCode() = h.ToString() |> hash

    member h.ToInt() =
        let (Hex n) = h in Int32.Parse(n, Globalization.NumberStyles.HexNumber)

    member h.ToInt64() =
        let (Hex n) = h in Int64.Parse(n, Globalization.NumberStyles.HexNumber)

    member h.ToByte() =
        let (Hex n) = h in Byte.Parse(n, Globalization.NumberStyles.HexNumber)

    member h.ToUInt() =
        let (Hex n) = h in UInt32.Parse(n, Globalization.NumberStyles.HexNumber)

    member h.ToUInt64() =
        let (Hex n) = h in UInt64.Parse(n, Globalization.NumberStyles.HexNumber)

    override h.Equals(a) =
        match a with
        | :? MemoryAddress as x -> x.ToUInt64() = h.ToUInt64()
        | _ -> false

    interface System.IComparable with
        member h.CompareTo a =
            match a with
            | :? MemoryAddress as x -> compare (h.ToUInt64()) (x.ToUInt64())
            | :? uint64 as x -> compare (h.ToUInt64()) x
            | _ -> invalidArg (nameof a) "Can not compare to this type."

[<AutoOpen>]
module MemoryAddressTopLevelOperations =
    let validateStr s =
        if isNullOrEmpty s then
            invalidArg (nameof s) $"A memory address can not be an empty string."

        s

    let getValue s =
        let non0x = Regex("(0[xX])?(.*)").Match(s).Groups[2].Value

        if Regex("[^a-fA-F0-9]").Match(non0x).Success then
            invalidArg (nameof s) $"{s} is not a valid memory address."

        non0x

    let removeTrailingZeros s =
        Regex("(0*)(.*)").Match(s).Groups[2].Value

    // Constructor
    let MemoryAddress (s: obj) =
        let v =
            match s with
            | :? byte as x -> x.ToString("X")
            | :? uint as x -> x.ToString("X")
            | :? uint64 as x -> x.ToString("X")
            | :? string as s ->
                s
                |> validateStr
                |> getValue
                |> removeTrailingZeros
            | _ -> invalidArg (nameof s) $"Only byte, uint, uint64 and string can be converted to a memory address."

        MemoryAddress.Create(v.ToLower())
