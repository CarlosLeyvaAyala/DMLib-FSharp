namespace DMLib.Types

[<Sealed>]
type NonEmptyString =
    member Value: unit -> string
    static member Create: s: string -> NonEmptyString

[<AutoOpen>]
module NonEmptyStringConstructor =
    val inline NonEmptyString: string -> NonEmptyString
