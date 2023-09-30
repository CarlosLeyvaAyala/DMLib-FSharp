namespace DMLib

open System

[<AutoOpen>]
module ArrayMisc =
    let (|ArrayLengthIs|_|) len a =
        if Array.length a = len then
            Some()
        else
            None

    let (|EmptyArray|OneElemArray|ManyElemArray|) a =
        match a with
        | [||] -> EmptyArray
        | [| x |] -> OneElemArray(x)
        | _ ->
            let h = a[0]
            let t = a[1..]
            ManyElemArray(h, t)

[<RequireQualifiedAccess>]
module Array =
    let (|ArrayLengthIs|_|) = ArrayMisc.(|ArrayLengthIs|_|)

    let (|EmptyArray|OneElemArray|ManyElemArray|) =
        ArrayMisc.(|EmptyArray|OneElemArray|ManyElemArray|)

    let duplicates a =
        a
        |> Array.groupBy id
        |> Array.choose (fun (key, set) ->
            if set.Length > 1 then
                Some key
            else
                None)

    [<Obsolete "Use duplicates instead">]
    let getDuplicates = duplicates

    /// Returns the first element in the array.
    let first =
        function
        | EmptyArray -> None
        | OneElemArray x -> Some x
        | ManyElemArray (x, _) -> Some x

    /// Returns the first element in the array or the default provided value.
    let firstOr value a =
        match a with
        | EmptyArray -> value
        | OneElemArray x -> x
        | ManyElemArray (x, _) -> x


    [<RequireQualifiedAccess>]
    module Parallel =
        let filter predicate array =
            array
            |> Array.Parallel.choose (fun x -> if predicate x then Some x else None)

        let duplicates a =
            a
            |> Array.groupBy id
            |> Array.Parallel.choose (fun (key, set) ->
                if set.Length > 1 then
                    Some key
                else
                    None)
