namespace DMLib.Types.Skyrim

[<Sealed>]
type EspFileName =
    interface System.IComparable
    new: filename: string -> EspFileName
    override Equals: a: obj -> bool
    override GetHashCode: unit -> int
    override ToString: unit -> string
    member Value: string

[<AutoOpen>]
module EspFileNameConstructor =
    val EspFileName: fileName: string -> EspFileName
