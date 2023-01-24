namespace DMLib.Types

[<CustomEquality; CustomComparison>]
type RecordId =
    | R of uint64
    member r.ToUInt64() = let (R n) = r in n
    override r.ToString() = sprintf "RecordId %d" (r.ToUInt64())
    static member Create(n) = R n
    member r.Increment() = r.ToUInt64() + 1UL |> R
    override r.GetHashCode() = r.ToUInt64() |> hash

    override r.Equals(a) =
        match a with
        | :? RecordId as x -> x.ToUInt64() = r.ToUInt64()
        | _ -> false

    interface System.IComparable with
        member r.CompareTo a =
            match a with
            | :? RecordId as x -> compare (r.ToUInt64()) (x.ToUInt64())
            | _ -> invalidArg (nameof a) "Can not compare arguments of different types."

[<AutoOpen>]
module RecordIdTopLevelOperations =
    let inline RecordId a =
        if int a < 0 then
            invalidArg (nameof a) $"A record id can not be negative. {a} is not a valid record id."

        RecordId.Create(uint64 a)
