namespace DMLib.Types.Skyrim

/// <summary>Weight for a Skyrim <c>Actor</c>. Its value is always between [0,100] because that's what the Skyrim engine allows.</summary>
[<Sealed>]
type Weight =
    interface System.IComparable
    new: value: int -> Weight
    override Equals: a: obj -> bool
    override GetHashCode: unit -> int
    member ToInt: unit -> int
    override ToString: unit -> string
    member Value: int
    static member Max: int
    static member Min: int

[<AutoOpen>]
module WeightConstructor =
    ///<summary>Creates a new <c>Weight</c> from a numeric value.</summary>
    ///<exception cref="T:System.ArgumentException">When the input number is outside the [0,100] range.</exception>
    val inline Weight: a: int -> Weight
