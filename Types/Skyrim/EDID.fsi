namespace DMLib.Types.Skyrim

[<Sealed>]
type EDID =
    member Value: unit -> string
    static member Create: edid: string -> EDID

[<AutoOpen>]
module EDIDConstructor =
    val EDID: s: string -> EDID
