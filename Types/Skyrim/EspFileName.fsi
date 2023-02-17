namespace DMLib.Types.Skyrim

[<Sealed>]
type EspFileName =
    member Value: unit -> string
    static member Create: x: string -> EspFileName

module EspFileNameTopLevelOperations =
    val EspFileName: fileName: string -> EspFileName
