namespace DMLib.Types.Skyrim

open System
open DMLib.Types
open EspFileNameTopLevelOperations

[<AutoOpen>]
module private Helpers =
    let separator = "|"

    let separate (s: string) =
        let a = s.Split(separator)
        (a[0], a[1])

    let construct esp formId = sprintf "%s%s%s" esp separator formId

[<CustomEquality; CustomComparison>]
type UniqueId =
    | UId of string
    static member Create(x) = UId x
    member h.Value() = let (UId x) = h in x
    override h.ToString() = h.Value()
    override h.GetHashCode() = h.ToString() |> hash
    member h.Split() = () |> h.ToString |> separate

    override h.Equals(a) =
        match a with
        | :? UniqueId as x -> x.ToString() = h.ToString()
        | _ -> false

    interface System.IComparable with
        member h.CompareTo a =
            match a with
            | :? UniqueId as x ->
                let esp1, id1 = h.Split()
                let esp2, id2 = x.Split()

                match compare esp1 esp2 with
                | z when z = 0 ->
                    let i1 = UInt32.Parse(id1, Globalization.NumberStyles.HexNumber)
                    let i2 = UInt32.Parse(id2, Globalization.NumberStyles.HexNumber)
                    compare i1 i2
                | c -> c

            | _ -> invalidArg (nameof a) "Can not compare arguments of different types."

[<AutoOpen>]
module UniqueIdTopLevelOperations =
    let validate (s: string) =
        if not (s.Contains(separator)) then
            let m = construct "File name.esx" "Hex number"

            let mm =
                sprintf "An unique id must have the form \"%s\". \"%s\" is not a valid value." m s

            invalidArg (nameof s) mm

        let esp, formId = separate s

        // These will create an exception if the value is invalid
        let e = (EspFileName esp).Value()
        let f = (MemoryAddress formId).Value()
        construct e f

    let UniqueId s = s |> validate |> UId
