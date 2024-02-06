[<RequireQualifiedAccess>]
module DMLib.List

open System

let duplicates l =
    l
    |> List.groupBy id
    |> List.choose (fun (key, set) -> if set.Length > 1 then Some key else None)

[<Obsolete "Use duplicates instead">]
let getDuplicates = duplicates

let insertDistinctAt index value source =
    source |> List.insertAt index value |> List.distinct

let insertManyDistinctAt index values source =
    source |> List.insertManyAt index values |> List.distinct

open System.Collections.Generic

/// Partitions a sequence of Result into two lists
let partitionResult xs =
    let oks = List<'T>()
    let errors = List<'V>()

    for x in xs do
        match x with
        | Ok v -> oks.Add v
        | Error e -> errors.Add e

    let l x = x |> seq |> List.ofSeq

    (l oks, l errors)
