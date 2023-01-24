namespace DMLib.Types

///<summary>A positive number that goes from <c>0</c> onwards.</summary>
[<Sealed>]
type RecordId =
    interface System.IComparable
    ///<summary>Creates a new <c>RecordId</c> adding <c>+1</c> to current value.</summary>
    member Increment: unit -> RecordId
    ///<summary>Converts value to string.</summary>
    override ToString: unit -> string
    ///<summary>Converts value to unsigned 64-bits int.</summary>
    member ToUInt64: unit -> uint64
    ///<summary>Creates a new <c>RecordId</c> from an unsigned 64-bits int.</summary>
    static member Create: n: uint64 -> RecordId
    override Equals: a: obj -> bool
    override GetHashCode: unit -> int

[<AutoOpen>]
module RecordIdTopLevelOperations =
    ///<summary>Creates a new <c>RecordId</c> from a numeric value.</summary>
    ///<exception cref="T:System.ArgumentException">When the input number is negative.</exception>
    val inline RecordId: a: ^a -> RecordId
        when ^a: (static member op_Explicit: ^a -> int) and ^a: (static member op_Explicit: ^a -> uint64)
