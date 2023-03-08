namespace DMLib.Types.Skyrim

open DMLib.MathL
open DMLib.Combinators

[<AutoOpen>]
module private H =
    let minW = 0
    let maxW = 100

[<Sealed>]
type Weight(value: int) =
    static let inValidRange = isInRange minW maxW

    static let validate a =
        if a |> (isNot inValidRange) then
            invalidArg (nameof a) $"A Skyrim weight must be between {minW} and {maxW}. {a} is not a valid weight."

        a

    let v = validate value
    static member Min = minW
    static member Max = maxW
    member w.Value = v
    member w.ToInt() = v
    override w.ToString() = sprintf "Weight %d" v
    override w.GetHashCode() = v.ToString() |> hash

    override w.Equals(a) =
        match a with
        | :? Weight as x -> x.Value = w.Value
        | _ -> false

    interface System.IComparable with
        member w.CompareTo a =
            match a with
            | :? Weight as x -> compare w.Value x.Value
            | _ -> invalidArg (nameof a) "Can not compare arguments of different types."

[<AutoOpen>]
module WeightConstructor =
#if INTERACTIVE
    fsi.AddPrinter(fun (r: Weight) -> r.ToString())
#endif
    let inline Weight a = Weight(a)
