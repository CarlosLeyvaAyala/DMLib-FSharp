namespace DMLib.Types

/// <summary>An hexadecimal value.</summary>
/// <remarks>This type has basic support for comparing it to <c>int</c> types, but it may cause problems when comparing negative numbers, since it is not the intention for this type to store negative numbers, but memory addresses.</remarks>
[<Sealed>]
type HexNumber =
    interface System.IComparable
    override Equals: a: obj -> bool
    override GetHashCode: unit -> int
    member ToInt: unit -> int
    member ToInt64: unit -> int64
    override ToString: unit -> string
    static member Create: x: string -> HexNumber

[<AutoOpen>]
module HexNumberTopLevelOperations =
    ///<summary> Creates a new <c>HexNumber</c> from a <c>string</c>, <c>int</c> or <c>int64</c>.
    ///Using a leading <c>0x</c> on <c>string</c> inputs is optional.</summary>
    ///<exception cref="T:System.ArgumentException">When the input is not a supported integer type or a valid string.</exception>
    val inline HexNumber: s: obj -> HexNumber
