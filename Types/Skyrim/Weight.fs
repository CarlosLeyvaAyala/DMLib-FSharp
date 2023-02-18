namespace DMLib.Types.Skyrim

open DMLib.MathL
open DMLib.Combinators

[<Sealed>]
type Weight(value: int) =
    let inValidRange = isInRange 0 100

    let validate a =
        if a |> (isNot inValidRange) then
            invalidArg (nameof a) $"A Skyrim weight must be between 0 and 100. {a} is not a valid weight."

        a

    let v = validate value
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
