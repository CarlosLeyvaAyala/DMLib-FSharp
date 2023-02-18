namespace DMLib.Types

/// <summary>A positive number that goes from <c>0</c> onwards.</summary>
[<Sealed>]
type RecordId =
    interface System.IComparable
    new: id: uint64 -> RecordId
    override Equals: a: obj -> bool
    override GetHashCode: unit -> int
    /// <summary>Creates a new <c>RecordId</c> adding <c>+1</c> to current value.</summary>
    member Increment: unit -> RecordId
    /// <summary>Converts value to string.</summary>
    override ToString: unit -> string
    /// <summary>Converts value to unsigned 64-bits int.</summary>
    member ToUInt64: unit -> uint64
    /// <summary>Creates a new <c>RecordId</c> adding <c>+1</c> to current value.</summary>
    member Next: (unit -> RecordId)
    member Value: uint64


[<AutoOpen>]
module RecordIdConstructor =
    ///<summary>Creates a new <c>RecordId</c> from an uint64.</summary>
    val inline RecordId: a: uint64 -> RecordId
