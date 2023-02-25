[<RequireQualifiedAccess>]
module DMLib.Dictionary

open System.Collections.Generic
open System.Threading.Tasks
open System

let iter action (dict: Dictionary<'T, 'U>) =
    for k in dict do
        action k.Key k.Value

let map mapping (dict: Dictionary<'T, 'U>) =
    for k in dict do
        dict[k.Key] <- mapping k.Key k.Value

    dict

/// Returns an array with the filtered data.
let filter (predicate: 'T -> 'U -> bool) (dict: Dictionary<'T, 'U>) =
    [| for k in dict do
           let k' = k.Key
           let v = k.Value
           if predicate k' v then (k', v) |]


let toArray (dict: Dictionary<'T, 'U>) =
    [| for k in dict do
           (k.Key, k.Value) |]

let toArrayP (dict: Dictionary<'T, 'U>) =
    let a: ('T * 'U) array = Array.zeroCreate dict.Count
    let mutable n = 0

    let add (i: KeyValuePair<'T, 'U>) =
        a[n] <- (i.Key, i.Value)
        n <- n + 1

    let f = Action<KeyValuePair<'T, 'U>> add
    Parallel.ForEach(dict, add) |> ignore
    a


let toList (dict: Dictionary<'T, 'U>) =
    [ for k in dict do
          (k.Key, k.Value) ]

let toSeq (dict: Dictionary<'T, 'U>) =
    seq {
        for k in dict do
            (k.Key, k.Value)
    }

let toMap (dict: Dictionary<'T, 'U>) =
    let mutable m: Map<'T, 'U> = Map.empty

    for k in dict do
        m <- m.Add(k.Key, k.Value)

    m

let addOrReplace key value (dict: Dictionary<'T, 'U>) =
    match dict.ContainsKey key with
    | false -> dict.Add(key, value)
    | true -> dict[key] <- value

    ()

let tryFind key (dict: Dictionary<'T, 'U>) =
    let sucess, v = dict.TryGetValue(key)

    match sucess with
    | true -> Some v
    | false -> None

let ofMap (map: Map<'T, 'U>) =
    let dict = Dictionary<'T, 'U>()
    map |> Map.iter (fun k v -> dict.Add(k, v))
    dict

let ofArray (array: ('T * 'U) array) =
    let dict = Dictionary<'T, 'U>()
    array |> Array.iter (fun (k, v) -> dict.Add(k, v))
    dict

let ofList (array: ('T * 'U) list) =
    let dict = Dictionary<'T, 'U>()
    array |> List.iter (fun (k, v) -> dict.Add(k, v))
    dict

let add key value (dict: Dictionary<'T, 'U>) = dict.Add(key, value)

let remove key (dict: Dictionary<'T, 'U>) = dict.Remove(key)
