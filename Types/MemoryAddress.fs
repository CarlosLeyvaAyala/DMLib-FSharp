namespace DMLib.Types

open System.Text.RegularExpressions
open DMLib.String
open System

//[<CustomEquality; CustomComparison>]
[<Sealed>]
type MemoryAddress(address: string) =
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

    let v =
        address
        |> validateStr
        |> getValue
        |> removeTrailingZeros
        |> toLower

    override h.GetHashCode() = h.ToString() |> hash
    override h.ToString() = sprintf "MemoryAddress %s" v

    member h.ToInt() =
        Int32.Parse(v, Globalization.NumberStyles.HexNumber)

    member h.ToInt64() =
        Int64.Parse(v, Globalization.NumberStyles.HexNumber)

    member h.ToByte() =
        Byte.Parse(v, Globalization.NumberStyles.HexNumber)

    member h.ToUInt() =
        UInt32.Parse(v, Globalization.NumberStyles.HexNumber)

    member h.ToUInt64() =
        UInt64.Parse(v, Globalization.NumberStyles.HexNumber)

    member h.Value = v

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

    new(address: byte) = MemoryAddress(address.ToString("X"))
    new(address: uint) = MemoryAddress(address.ToString("X"))
    new(address: uint64) = MemoryAddress(address.ToString("X"))

[<AutoOpen>]
module MemoryAddressConstructor =
    let MemoryAddress (s: string) = MemoryAddress(s)
