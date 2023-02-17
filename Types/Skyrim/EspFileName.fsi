namespace DMLib.Types.Skyrim

[<Sealed>]
type EspFileName =
    member Value: unit -> string
    static member Create: x: string -> EspFileName

[<AutoOpen>]
module EspFileNameTopLevelOperations =
    val EspFileName: fileName: string -> EspFileName
