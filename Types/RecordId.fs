namespace DMLib.Types

[<Sealed>]
type RecordId(id: uint64) =
    let v = id
    member _.Value = v
    member _.ToUInt64() = v
    override _.ToString() = sprintf "RecordId %d" v
    member _.Increment() = RecordId(v + 1UL)
    member r.Next = r.Increment
    override _.GetHashCode() = hash v

    override r.Equals(a) =
        match a with
        | :? RecordId as x -> x.Value = r.Value
        | _ -> false

    interface System.IComparable with
        member r.CompareTo a =
            match a with
            | :? RecordId as x -> compare r.Value x.Value
            | _ -> invalidArg (nameof a) "Can not compare arguments of different types."

[<AutoOpen>]
module RecordIdConstructor =
#if INTERACTIVE
    fsi.AddPrinter(fun (r: RecordId) -> r.ToString())
#endif
    //let inline RecordId a = RecordId(a)
    let rid a = RecordId(a)

[<RequireQualifiedAccess>]
module RecordId =
    let first = RecordId(1UL)
    let next (r: RecordId) = r.Next()
    let value (r: RecordId) = r.Value

type RecordId with
    static member First = RecordId.first
    static member GetNext = RecordId.next
