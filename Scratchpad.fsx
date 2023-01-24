open System.Collections.ObjectModel

/// Converts some F# sequence to a C# list
let private XToClist fill =
    let l = System.Collections.Generic.List<'a>()
    fill l
    l

/// Converts an array to a C# list.
let ArrayToCList (a: 'a []) =
    XToClist(fun l -> a |> Array.iter (fun i -> l.Add(i)))

/// Converts an array to a C# list.
let ListToCList (a: List<'a>) =
    XToClist(fun l -> a |> List.iter (fun i -> l.Add(i)))

[| 0..3 |] |> ArrayToCList
[ 0..3 ] |> ListToCList
