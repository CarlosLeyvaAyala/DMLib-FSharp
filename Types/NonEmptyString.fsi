namespace DMLib.Types

[<Sealed>]
type NonEmptyString =
    interface System.IComparable
    new: s: string -> NonEmptyString
    override Equals: a: obj -> bool
    override GetHashCode: unit -> int
    override ToString: unit -> string
    member Value: string
    static member (+): a: NonEmptyString * b: NonEmptyString -> NonEmptyString
    static member (+): a: NonEmptyString * b: string -> NonEmptyString
    static member (+): a: string * b: NonEmptyString -> NonEmptyString

[<AutoOpen>]
module NonEmptyStringConstructor =
    val inline NonEmptyString: string -> NonEmptyString
