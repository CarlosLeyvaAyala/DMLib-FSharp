namespace DMLib.Types.Skyrim

[<Sealed>]
type UniqueId =
    interface System.IComparable
    override Equals: a: obj -> bool
    override GetHashCode: unit -> int
    member Split: unit -> string * string
    override ToString: unit -> string
    member Value: unit -> string
    static member Create: x: string -> UniqueId

[<AutoOpen>]
module UniqueIdTopLevelOperations =
    val UniqueId: s: string -> UniqueId
