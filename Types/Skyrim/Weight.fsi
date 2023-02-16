﻿namespace DMLib.Types.Skyrim

/// <summary>Weight for a Skyrim <c>Actor</c>. Its value is always between [0,100] because that's what the Skyrim engine allows.</summary>
[<Sealed>]
type Weight =
    interface System.IComparable
    override Equals: a: obj -> bool
    override GetHashCode: unit -> int
    member ToInt: unit -> int
    override ToString: unit -> string
    static member Create: x: int -> Weight

[<AutoOpen>]
module WeightTopLevelOperations =
    ///<summary>Creates a new <c>Weight</c> from a numeric value.</summary>
    ///<exception cref="T:System.ArgumentException">When the input number is outside the [0,100] range.</exception>
    val inline Weight: a: int -> Weight