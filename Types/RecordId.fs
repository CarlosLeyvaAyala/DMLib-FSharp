namespace DMLib.Types

[<Sealed>]
type RecordId(id: uint64) =
    let v = id
    member r.Value = v
    member r.ToUInt64() = v
    override r.ToString() = sprintf "RecordId %d" v
    member r.Increment() = RecordId(v + 1UL)
    member r.Next = r.Increment
    override r.GetHashCode() = hash v

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
    let inline RecordId a = RecordId(a)
