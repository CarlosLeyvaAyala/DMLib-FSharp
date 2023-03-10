module DMLib.Array

let (|EmptyArray|OneElemArray|ManyElemArray|) a =
    if Array.length a = 0 then
        EmptyArray
    elif Array.length a = 1 then
        OneElemArray(a)
    else
        let h = a[0]
        let t = a[1..]
        ManyElemArray(h, t)

let getDuplicates l =
    l
    |> Array.groupBy id
    |> Array.choose (fun (key, set) ->
        if set.Length > 1 then
            Some key
        else
            None)
