namespace DMLib.Types.Skyrim

open System
open DMLib.Types
open EspFileNameConstructor
open DMLib.String
open DMLib.Combinators

[<Sealed>]
type UniqueId(uId: string) =
    static let separator = "|"
    static let construct esp formId = sprintf "%s%s%s" esp separator formId

    static let makeValidId (esp, formId) =
        // These will create an exception if any of the values is invalid
        let e = (EspFileName esp).Value
        let f = (MemoryAddress formId).Value
        construct e f

    static let getInvalidFmtMsg s =
        let m = construct "File name.esx" "Hex number"
        sprintf "An unique id must have the form \"%s\". \"%s\" is not a valid value." m s

    static let separate (s: string) =
        let a = s.Split(separator)
        (a[0], a[1])

    static let validate (s: string) =
        if s |> dont (contains separator) then
            invalidArg (nameof s) (getInvalidFmtMsg s)

        separate s |> makeValidId

    let v = validate uId

    member _.Value = v
    override _.ToString() = v
    override _.GetHashCode() = hash v
    member _.Split() = separate v
    new(esp: string, formId: string) = UniqueId(makeValidId (esp, formId))
    new(esp: string, formId: byte) = UniqueId(esp, formId.ToString("x"))
    new(esp: string, formId: int16) = UniqueId(esp, formId.ToString("x"))
    new(esp: string, formId: int32) = UniqueId(esp, formId.ToString("x"))
    new(esp: string, formId: int64) = UniqueId(esp, formId.ToString("x"))
    new(esp: string, formId: sbyte) = UniqueId(esp, formId.ToString("x"))
    new(esp: string, formId: uint16) = UniqueId(esp, formId.ToString("x"))
    new(esp: string, formId: uint32) = UniqueId(esp, formId.ToString("x"))
    new(esp: string, formId: uint64) = UniqueId(esp, formId.ToString("x"))
    new(esp: string, formId: nativeint) = UniqueId(esp, formId.ToString("x"))
    new(esp: string, formId: unativeint) = UniqueId(esp, formId.ToString("x"))

    override s.Equals(a) =
        match a with
        | :? UniqueId as x -> x.Value = s.Value
        | _ -> false

    interface System.IComparable with
        member s.CompareTo a =
            match a with
            | :? UniqueId as x ->
                let esp1, id1 = s.Split()
                let esp2, id2 = x.Split()

                match compare esp1 esp2 with
                | z when z = 0 ->
                    let i1 = UInt32.Parse(id1, Globalization.NumberStyles.HexNumber)
                    let i2 = UInt32.Parse(id2, Globalization.NumberStyles.HexNumber)
                    compare i1 i2
                | c -> c

            | _ -> invalidArg (nameof a) "Can not compare arguments of different types."

[<AutoOpen>]
module UniqueIdConstructor =
#if INTERACTIVE
    fsi.AddPrinter(fun (r: UniqueId) -> r.ToString())
#endif
    let UniqueId s = UniqueId(s)
