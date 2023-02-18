namespace DMLib.Types

[<Sealed>]
type NonEmptyString =
    interface System.IComparable
    new: s: string -> NonEmptyString
    override Equals: a: obj -> bool
    override GetHashCode: unit -> int
    override ToString: unit -> string
    member Value: string

[<AutoOpen>]
module NonEmptyStringConstructor =
    val inline NonEmptyString: string -> NonEmptyString
