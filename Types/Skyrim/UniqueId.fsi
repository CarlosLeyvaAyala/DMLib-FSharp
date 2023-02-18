namespace DMLib.Types.Skyrim

[<Sealed>]
type UniqueId =
    interface System.IComparable
    new: uId: string -> UniqueId
    new: esp: string * formId: string -> UniqueId
    override Equals: a: obj -> bool
    override GetHashCode: unit -> int
    member Split: unit -> string * string
    override ToString: unit -> string
    member Value: string

[<AutoOpen>]
module UniqueIdConstructor =
    val UniqueId: s: string -> UniqueId
