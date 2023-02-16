namespace DMLib.Types.Skyrim

open DMLib.MathL
open DMLib.Combinators

[<CustomEquality; CustomComparison>]
type Weight =
    | W of int
    static member Create(x) = W x
    member w.ToInt() = let (W x) = w in x
    override w.ToString() = sprintf "Weight %d" (w.ToInt())
    override w.GetHashCode() = w.ToInt() |> hash

    override w.Equals(a) =
        match a with
        | :? Weight as x -> x.ToInt() = w.ToInt()
        | _ -> false

    interface System.IComparable with
        member w.CompareTo a =
            match a with
            | :? Weight as x -> compare (w.ToInt()) (x.ToInt())
            | _ -> invalidArg (nameof a) "Can not compare arguments of different types."

[<AutoOpen>]
module WeightTopLevelOperations =
    // Constructor
    let inline Weight a =
        let validRange = isInRange 0 100

        if a |> (isNot validRange) then
            invalidArg (nameof a) $"A Skyrim weight must be between 0 and 100. {a} is not a valid weight."

        Weight.Create(int a)
