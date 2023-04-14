namespace DMLib.Types.Skyrim

[<Sealed>]
type UniqueId =
    interface System.IComparable
    new: uId: string -> UniqueId
    new: esp: string * formId: string -> UniqueId
    new: esp: string * formId: byte -> UniqueId
    new: esp: string * formId: int16 -> UniqueId
    new: esp: string * formId: int32 -> UniqueId
    new: esp: string * formId: int64 -> UniqueId
    new: esp: string * formId: sbyte -> UniqueId
    new: esp: string * formId: uint16 -> UniqueId
    new: esp: string * formId: uint32 -> UniqueId
    new: esp: string * formId: uint64 -> UniqueId
    new: esp: string * formId: nativeint -> UniqueId
    new: esp: string * formId: unativeint -> UniqueId
    override Equals: a: obj -> bool
    override GetHashCode: unit -> int
    member Split: unit -> string * string
    static member Split: string -> string * string
    static member Separator: string
    override ToString: unit -> string
    member Value: string

[<AutoOpen>]
module UniqueIdConstructor =
    val UniqueId: s: string -> UniqueId
