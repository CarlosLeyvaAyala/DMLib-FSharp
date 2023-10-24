namespace DMLib

open System
open Combinators

[<AutoOpen>]
module ArrayMisc =
    let (|ArrayLengthIs|_|) len a =
        if Array.length a = len then
            Some()
        else
            None

    /// An old version used to send an array for OneElemArray
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

    /// Pipeable version of Array.append.
    let append' a b = swap Array.append a b

    /// Shuffles an array content
    let shuffle a =
        let r = Array.copy a

        let swap i j (array: _ []) =
            let tmp = array.[i]
            array.[i] <- array.[j]
            array.[j] <- tmp

        let rnd = System.Random()
        let n = Array.length r

        for i in [ 0 .. (n - 1) ] do
            let j = rnd.Next(i, n)
            swap i j r

        r

    let getRandomElement (a: 'a array) =
        a |> Array.item (Random().Next a.Length)

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

        /// Gets the elements that are have a key in a map.
        let filterByMapKeys map a =
            a
            |> filter (fun v -> map |> Map.tryFind v |> Option.isSome)

        /// Gets the elements that are have a key in a map.
        let chooseByMapKeys map mapper a =
            a
            |> Array.Parallel.choose (fun v -> map |> Map.tryFind v |> Option.map mapper)
