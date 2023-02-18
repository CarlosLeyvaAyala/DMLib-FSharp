namespace DMLib.Types.Skyrim

[<Sealed>]
type EDID =
    interface System.IComparable
    new: edid: string -> EDID
    override Equals: a: obj -> bool
    override GetHashCode: unit -> int
    override ToString: unit -> string
    member Value: string

[<AutoOpen>]
module EDIDConstructor =
    val EDID: s: string -> EDID
