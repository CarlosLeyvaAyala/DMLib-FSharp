﻿namespace DMLib.Types

/// <summary>A positive hexadecimal value.</summary>
/// <remarks>This type will always be compared as a <c>uint64</c> to avoid overflow exceptions, but functions for converting to other types are provided for convenience.</remarks>
[<Sealed>]
type MemoryAddress =
    interface System.IComparable
    new: address: string -> MemoryAddress
    new: address: byte -> MemoryAddress
    new: address: uint16 -> MemoryAddress
    new: address: uint32 -> MemoryAddress
    new: address: uint64 -> MemoryAddress
    override Equals: a: obj -> bool
    override GetHashCode: unit -> int
    member ToByte: unit -> byte
    member ToInt: unit -> int
    member ToInt64: unit -> int64
    override ToString: unit -> string
    member ToUInt: unit -> uint32
    member ToUInt64: unit -> uint64
    member Value: string

[<AutoOpen>]
module MemoryAddressConstructor =
    ///<summary> Creates a new <c>HexNumber</c> from a <c>string</c>, <c>int</c> or <c>int64</c>.
    ///Using a leading <c>0x</c> on <c>string</c> inputs is optional.</summary>
    ///<exception cref="T:System.ArgumentException">When the input is not a supported integer type or a valid string.</exception>
    val MemoryAddress: s: string -> MemoryAddress
